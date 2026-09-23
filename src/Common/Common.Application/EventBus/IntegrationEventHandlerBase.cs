using System.Diagnostics;
using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.IntegrationEvents;
using EntityFramework.Exceptions.Common;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Common.Application.EventBus;

/// <summary>
///     Idempotent consumer base. Two layers, in order:
///     1. FusionCache key (fast pre-filter, best effort: distributed cache errors are swallowed by FusionCache and
///        its stampede lock is in-process only).
///     2. Transactional inbox: a <see cref="ProcessedMessage"/> row is tracked in the consuming module's DbContext
///        before <see cref="ProcessAsync"/> runs, so the handler's own SaveChangesAsync commits the mark and the
///        side effects atomically. A redelivery after commit, or a second instance, hits the composite primary key
///        and is acknowledged as a duplicate. Pass the store of your own module:
///        <c>IInboxStore&lt;IInventoryDbContext&gt;</c>.
///     The cache key is only written after a successful commit.
/// </summary>
public abstract partial class IntegrationEventHandlerBase<TEvent>(
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    IInboxStore inbox,
    ILogger logger
) : IConsumer<TEvent>
    where TEvent : IntegrationEvent
{
    protected virtual TimeSpan? MaxEventAge => null;

    public async Task Consume(ConsumeContext<TEvent> context)
    {
        var @event = context.Message;
        var cancellationToken = context.CancellationToken;
        var eventType = typeof(TEvent).Name;
        var eventId = @event.Id;
        var consumerName = GetType().Name;

        if (MaxEventAge is { } maxAge)
        {
            var age = DateTimeOffset.UtcNow - @event.CreatedOn;
            if (age > maxAge)
            {
                Activity.Current?.SetTag("event.outcome", "stale");
                LogStaleEvent(logger, eventType, eventId, age, maxAge);
                return;
            }
        }

        var key = $"processed_event:{consumerName}:{eventId}";

        // A fresh options instance on purpose, never DefaultEntryOptions.Duplicate(): the configured defaults
        // carry factory soft/hard timeouts and fail-safe settings meant for read-through entries.
        var entryOptions = new FusionCacheEntryOptions
        {
            // L1 bound: duplicates cluster within minutes; keeping every key in process
            // memory for the full IdempotencyKeyDuration grows unbounded with event volume.
            Duration = cachingOptions.Value.IdempotencyL1Duration,
            // L2 (Redis, when configured) holds the key for the full idempotency window.
            DistributedCacheDuration = cachingOptions.Value.IdempotencyKeyDuration,
        };

        var cached = await cache.TryGetAsync<bool>(key, entryOptions, cancellationToken);
        if (cached.HasValue)
        {
            MarkDuplicate(eventType, eventId);
            return;
        }

        inbox.Mark(consumerName, eventId);

        try
        {
            LogProcessingStarted(logger, eventType, eventId);
            await ProcessAsync(@event, cancellationToken);
            await inbox.SaveIfPendingAsync(cancellationToken);
            LogProcessingCompleted(logger, eventType, eventId);
        }
        catch (UniqueConstraintException ex) when (ProcessedMessage.IsDuplicateKey(ex))
        {
            // Either the handler's own SaveChangesAsync or SaveIfPendingAsync hit the inbox key: the message
            // was committed by an earlier delivery, and this attempt's side effects rolled back with it.
            MarkDuplicate(eventType, eventId);
            return;
        }

        await cache.SetAsync(key, true, entryOptions, token: cancellationToken);
        Activity.Current?.SetTag("event.outcome", "processed");
    }

    protected abstract Task ProcessAsync(TEvent @event, CancellationToken cancellationToken);

    private void MarkDuplicate(string eventType, DefaultIdType eventId)
    {
        Activity.Current?.SetTag("event.outcome", "duplicate");
        LogDuplicateSkipped(logger, eventType, eventId);
    }

    [LoggerMessage(Level = LogLevel.Debug, Message = "Processing {EventType} (Id={MessageId}).")]
    private static partial void LogProcessingStarted(ILogger logger, string eventType, DefaultIdType messageId);

    [LoggerMessage(Level = LogLevel.Debug, Message = "Processed {EventType} (Id={MessageId}).")]
    private static partial void LogProcessingCompleted(ILogger logger, string eventType, DefaultIdType messageId);

    [LoggerMessage(Level = LogLevel.Information, Message = "Skipped duplicate {EventType} (Id={MessageId}): already processed.")]
    private static partial void LogDuplicateSkipped(ILogger logger, string eventType, DefaultIdType messageId);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Discarding stale {EventType} (Id={MessageId}): age {Age} exceeds MaxEventAge {MaxAge}. Message will not be retried.")]
    private static partial void LogStaleEvent(
        ILogger logger, string eventType, DefaultIdType messageId, TimeSpan age, TimeSpan maxAge);
}
