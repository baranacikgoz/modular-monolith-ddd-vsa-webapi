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
            await ProcessBatchAsync(stoppingToken);
            await Task.Delay(options.Value.PollIntervalMs, stoppingToken)
                .ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
        }
    }

    private async Task ProcessBatchAsync(CancellationToken ct)
    {
        using var scope = scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
        var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

        var opts = options.Value;
        var sw = Stopwatch.StartNew();

        await using var tx = await db.Database.BeginTransactionAsync(ct);
        try
        {
#pragma warning disable S2077 // SQL queries should not be vulnerable to injection attacks — parameters are safe
            var messages = await db.OutboxMessages
                .FromSqlRaw(
                    """
                    SELECT * FROM "Outbox"."OutboxMessages"
                    WHERE "IsProcessed" = false
                      AND "FailedOn" IS NULL
                      AND ("NextRetryAt" IS NULL OR "NextRetryAt" <= (NOW() AT TIME ZONE 'UTC'))
                    ORDER BY "CreatedOn"
                    LIMIT {0}
                    FOR UPDATE SKIP LOCKED
                    """,
                    opts.BatchSize)
                .ToListAsync(ct);
#pragma warning restore S2077

            OutboxTelemetry.PollBatchSize.Record(messages.Count);

            foreach (var message in messages)
            {
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

                    await publishEndpoint.Publish(integrationEvent, integrationEvent.GetType(), ct);
                    message.MarkAsProcessed(timeProvider.GetUtcNow());
                    OutboxTelemetry.MessagesPublished.Add(1);
                    activity?.SetStatus(ActivityStatusCode.Ok);
                    LogPublished(logger, message.Id, integrationEvent.GetType().Name);
                }
#pragma warning disable CA1031
                catch (Exception ex)
#pragma warning restore CA1031
                {
                    activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                    activity?.SetTag("exception.message", ex.Message);
                    activity?.SetTag("exception.type", ex.GetType().Name);
                    message.IncrementRetryCount(timeProvider.GetUtcNow(), ComputeBackoff(message.RetryCount, opts));
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
            await tx.CommitAsync(ct);
            OutboxTelemetry.ProcessingDuration.Record(sw.Elapsed.TotalMilliseconds);
        }
#pragma warning disable CA1031
        catch (Exception ex) when (!ct.IsCancellationRequested)
#pragma warning restore CA1031
        {
            await tx.RollbackAsync(CancellationToken.None);
            LogBatchError(logger, ex);
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
