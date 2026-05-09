using System.Diagnostics;
using Common.Application.Options;
using Common.Domain.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Common.Application.EventBus;

public abstract partial class EventHandlerBase<TEvent>(
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    ILogger logger
) where TEvent : class, IEvent
{
    protected virtual TimeSpan? MaxEventAge => null;

    public async Task HandleAsync(TEvent @event, CancellationToken cancellationToken)
    {
        var eventType = typeof(TEvent).Name;
        var eventId = @event.Id;

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

        var key = $"processed_event:{eventId}";
        var factoryRan = false;

        await cache.GetOrSetAsync<bool>(
            key,
            async (_, ct) =>
            {
                factoryRan = true;
                LogProcessingStarted(logger, eventType, eventId);
                await ProcessAsync(@event, ct);
                LogProcessingCompleted(logger, eventType, eventId);
                return true;
            },
            options: new FusionCacheEntryOptions { Duration = cachingOptions.Value.IdempotencyKeyDuration },
            token: cancellationToken);

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

    protected abstract Task ProcessAsync(TEvent @event, CancellationToken cancellationToken);

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
        Message =
            "Discarding stale {EventType} (Id={EventId}): age {Age} exceeds MaxEventAge {MaxAge}. Offset will be committed; message will not be retried.")]
    private static partial void LogStaleEvent(
        ILogger logger, string eventType, DefaultIdType eventId, TimeSpan age, TimeSpan maxAge);
}
