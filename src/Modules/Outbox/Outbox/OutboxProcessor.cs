using System.Diagnostics;
using Common.Application.Options;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Outbox.Persistence;
using Outbox.Telemetry;

namespace Outbox;

public sealed partial class OutboxProcessor(
    IServiceScopeFactory scopeFactory,
    IOptions<OutboxOptions> options,
    TimeProvider timeProvider,
    ILogger<OutboxProcessor> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Gate at runtime (not registration) so IsProcessor honours the fully-merged configuration,
        // including overrides supplied by tests. Integration test factories set IsProcessor = false to
        // keep this BackgroundService from opening "FOR UPDATE" transactions on OutboxMessages that
        // would otherwise deadlock Respawn's between-test reset. Outbox.Tests opts back in.
        if (!options.Value.IsProcessor)
        {
            LogProcessorDisabled(logger);
            return;
        }

        LogStarting(logger);

        while (!stoppingToken.IsCancellationRequested)
        {
            var processed = await ProcessBatchAsync(stoppingToken);

            // Drain fast when the queue is backed up: a full batch means more work is likely waiting,
            // so poll again immediately instead of idling out PollIntervalMs.
            if (processed >= options.Value.BatchSize)
            {
                continue;
            }

            await Task.Delay(options.Value.PollIntervalMs, stoppingToken)
                .ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
        }
    }

    // internal (not private): unit-tested directly from Outbox.Tests via InternalsVisibleTo, calling
    // ProcessBatchAsync once per test instead of racing the BackgroundService's poll-interval timer.
    internal async Task<int> ProcessBatchAsync(CancellationToken ct)
    {
        using var scope = scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
        var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

        var opts = options.Value;
        var sw = Stopwatch.StartNew();

        try
        {
            // Lease-claim pattern: this single atomic UPDATE ... RETURNING claims up to BatchSize rows by
            // pushing NextRetryAt into the future (the "lease") and returns them — no explicit transaction,
            // autocommit, so the FOR UPDATE SKIP LOCKED row locks release the instant this statement
            // commits. Publishing to RabbitMQ then happens with NO open transaction and NO held locks, so a
            // slow/down broker can no longer starve the connection pool. A crash between claiming and
            // publishing leaves the message leased-but-unpublished; once the lease (ClaimLeaseSeconds)
            // expires it's picked up again by any instance. That can produce duplicate publishes — this is
            // acceptable and by design, because all consumers inherit IntegrationEventHandlerBase (idempotent).
#pragma warning disable S2077 // SQL queries should not be vulnerable to injection attacks — parameters are safe
            var messages = await db.OutboxMessages
                .FromSqlRaw(
                    """
                    UPDATE "Outbox"."OutboxMessages" o
                    SET "NextRetryAt" = (NOW() AT TIME ZONE 'UTC') + make_interval(secs => {1})
                    FROM (
                        SELECT "Id" FROM "Outbox"."OutboxMessages"
                        WHERE "IsProcessed" = false
                          AND "FailedOn" IS NULL
                          AND ("NextRetryAt" IS NULL OR "NextRetryAt" <= (NOW() AT TIME ZONE 'UTC'))
                        ORDER BY "CreatedOn"
                        LIMIT {0}
                        FOR UPDATE SKIP LOCKED
                    ) c
                    WHERE o."Id" = c."Id"
                    RETURNING o.*
                    """,
                    opts.BatchSize,
                    opts.ClaimLeaseSeconds)
                .ToListAsync(ct);
#pragma warning restore S2077

            OutboxTelemetry.PollBatchSize.Record(messages.Count);

            var consecutiveFailures = 0;

            for (var i = 0; i < messages.Count; i++)
            {
                var message = messages[i];

                // Three publishes in a row have failed — the broker is very likely down. Stop attempting
                // the rest of this batch and release their claims immediately so they don't sit idle for
                // the full lease before becoming eligible again.
                if (consecutiveFailures >= 3)
                {
                    for (var j = i; j < messages.Count; j++)
                    {
                        messages[j].ReleaseClaim();
                    }

                    break;
                }

                Activity? activity;
                if (message.TraceId is not null && message.ParentSpanId is not null)
                {
                    var parentContext = new ActivityContext(
                        ActivityTraceId.CreateFromString(message.TraceId),
                        ActivitySpanId.CreateFromString(message.ParentSpanId),
                        ActivityTraceFlags.Recorded);
                    activity = OutboxTelemetry.ActivitySource.StartActivity(
                        "outbox.publish", ActivityKind.Producer, parentContext);
                }
                else
                {
                    activity = OutboxTelemetry.ActivitySource.StartActivity(
                        "outbox.publish", ActivityKind.Producer);
                }

                using var _ = activity;
                activity?.SetTag("outbox.message_id", message.Id);
                activity?.SetTag("outbox.retry_count", message.RetryCount);
                activity?.SetTag("event.type", message.Event?.GetType().Name);

                try
                {
                    var integrationEvent = message.Event;
                    if (integrationEvent is null)
                    {
                        LogNullEvent(logger, message.Id);
                        message.IncrementRetryCount(timeProvider.GetUtcNow(), ComputeBackoff(message.RetryCount, opts));
                        if (message.RetryCount >= opts.MaxRetryCount)
                        {
                            message.MarkAsFailed(timeProvider.GetUtcNow());
                            OutboxTelemetry.MessagesPermanentlyFailed.Add(1);
                        }

                        continue;
                    }

                    using var publishCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
                    publishCts.CancelAfter(opts.PublishTimeoutMs);
                    await publishEndpoint.Publish(integrationEvent, integrationEvent.GetType(), publishCts.Token);
                    message.MarkAsProcessed(timeProvider.GetUtcNow());
                    OutboxTelemetry.MessagesPublished.Add(1);
                    activity?.SetStatus(ActivityStatusCode.Ok);
                    LogPublished(logger, message.Id, integrationEvent.GetType().Name);
                    consecutiveFailures = 0;
                }
                catch (OperationCanceledException) when (ct.IsCancellationRequested)
                {
                    // Real shutdown (not just this message's PublishTimeoutMs) — propagate, don't treat
                    // it as a publish failure.
                    throw;
                }
#pragma warning disable CA1031
                catch (Exception ex)
#pragma warning restore CA1031
                {
                    // Either a genuine publish failure or the per-publish PublishTimeoutMs firing
                    // (OperationCanceledException with ct still live) — both go through the same
                    // retry/backoff path.
                    activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                    activity?.SetTag("exception.message", ex.Message);
                    activity?.SetTag("exception.type", ex.GetType().Name);
                    message.IncrementRetryCount(timeProvider.GetUtcNow(), ComputeBackoff(message.RetryCount, opts));
                    consecutiveFailures++;
                    if (message.RetryCount >= opts.MaxRetryCount)
                    {
                        message.MarkAsFailed(timeProvider.GetUtcNow());
                        OutboxTelemetry.MessagesPermanentlyFailed.Add(1);
                        LogPermanentlyFailed(logger, message.Id, ex);
                    }
                    else
                    {
                        LogRetryScheduled(logger, message.Id, message.RetryCount, opts.MaxRetryCount, ex);
                    }
                }
            }

            await db.SaveChangesAsync(ct);
            OutboxTelemetry.ProcessingDuration.Record(sw.Elapsed.TotalMilliseconds);
            return messages.Count;
        }
#pragma warning disable CA1031
        catch (Exception ex) when (!ct.IsCancellationRequested)
#pragma warning restore CA1031
        {
            LogBatchError(logger, ex);
            return 0;
        }
    }

    private static TimeSpan ComputeBackoff(int currentRetryCount, OutboxOptions opts)
    {
        var cap = Math.Min(opts.MaxBackoffSeconds, Math.Pow(2, currentRetryCount) * opts.BaseBackoffSeconds);
#pragma warning disable CA5394 // Jitter is not a security-sensitive operation; predictability is irrelevant here.
        var seconds = Random.Shared.NextDouble() * cap;
#pragma warning restore CA5394
        return TimeSpan.FromSeconds(seconds);
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "OutboxProcessor starting.")]
    private static partial void LogStarting(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "OutboxProcessor disabled (IsProcessor = false); not polling on this instance.")]
    private static partial void LogProcessorDisabled(ILogger logger);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Outbox message {MessageId} has null event — skipping.")]
    private static partial void LogNullEvent(ILogger logger, int messageId);

    [LoggerMessage(Level = LogLevel.Debug,
        Message = "Outbox message {MessageId} ({EventType}) published to RabbitMQ.")]
    private static partial void LogPublished(ILogger logger, int messageId, string eventType);

    [LoggerMessage(Level = LogLevel.Error,
        Message = "Outbox message {MessageId} permanently failed after exhausting retries.")]
    private static partial void LogPermanentlyFailed(ILogger logger, int messageId, Exception ex);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Outbox message {MessageId} publish failed (retry {RetryCount}/{MaxRetryCount}).")]
    private static partial void LogRetryScheduled(ILogger logger, int messageId, int retryCount, int maxRetryCount,
        Exception ex);

    [LoggerMessage(Level = LogLevel.Error, Message = "OutboxProcessor batch failed; will retry on next poll.")]
    private static partial void LogBatchError(ILogger logger, Exception ex);
}
