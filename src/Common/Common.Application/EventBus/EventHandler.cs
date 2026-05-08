using System.Diagnostics;
using Common.Application.Options;
using Common.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Common.Application.EventBus;

public abstract partial class EventHandlerBase<TEvent>(
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    ILogger logger
) : IEventHandler<TEvent> where TEvent : class, IEvent
{
    // Override in subclasses that need a staleness guard (e.g. financial handlers).
    // null = no check (default). Return silently on stale — do not throw, so the
    // Kafka offset is committed and the message is not retried.
    protected virtual TimeSpan? MaxEventAge => null;

    public async Task Consume(ConsumeContext<TEvent> context)
    {
        var eventType = typeof(TEvent).Name;
        var eventId = context.Message.Id;

        if (MaxEventAge is { } maxAge)
        {
            var age = DateTimeOffset.UtcNow - context.Message.CreatedOn;
            if (age > maxAge)
            {
                Activity.Current?.SetTag("event.outcome", "stale");
                LogStaleEvent(logger, eventType, eventId, age, maxAge);
                return;
            }
        }

        var key = $"processed_event:{eventId}";
        var factoryRan = false;

        // FusionCache with Redis backplane: factory executes at most once across all replicas for a given key.
        // If HandleAsync throws the entry is not persisted, so the message remains retry-eligible.
        await cache.GetOrSetAsync<bool>(
            key,
            async (_, ct) =>
            {
                factoryRan = true;
                LogProcessingStarted(logger, eventType, eventId);
                await HandleAsync(context, context.Message, ct);
                LogProcessingCompleted(logger, eventType, eventId);
                return true;
            },
            options: new FusionCacheEntryOptions { Duration = cachingOptions.Value.IdempotencyKeyDuration },
            token: context.CancellationToken);

        if (factoryRan)
        {
            Activity.Current?.SetTag("event.outcome", "processed");
        }
        else
        {
            Activity.Current?.SetTag("event.outcome", "duplicate");
            LogDuplicateSkipped(logger, eventType, eventId);
        }
    }

    protected abstract Task HandleAsync(ConsumeContext<TEvent> context, TEvent @event,
        CancellationToken cancellationToken);

    [LoggerMessage(Level = LogLevel.Debug,
        Message = "Processing {EventType} (Id={EventId}).")]
    private static partial void LogProcessingStarted(ILogger logger, string eventType, DefaultIdType eventId);

    [LoggerMessage(Level = LogLevel.Debug,
        Message = "Processed {EventType} (Id={EventId}).")]
    private static partial void LogProcessingCompleted(ILogger logger, string eventType, DefaultIdType eventId);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Skipped duplicate {EventType} (Id={EventId}): already processed.")]
    private static partial void LogDuplicateSkipped(ILogger logger, string eventType, DefaultIdType eventId);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Discarding stale {EventType} (Id={EventId}): age {Age} exceeds MaxEventAge {MaxAge}. Offset will be committed; message will not be retried.")]
    private static partial void LogStaleEvent(
        ILogger logger, string eventType, DefaultIdType eventId, TimeSpan age, TimeSpan maxAge);
}
