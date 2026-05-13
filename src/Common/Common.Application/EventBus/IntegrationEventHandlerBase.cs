using System.Diagnostics;
using Common.Application.Options;
using Common.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Common.Application.EventBus;

public abstract partial class IntegrationEventHandlerBase<TEvent>(
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
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
