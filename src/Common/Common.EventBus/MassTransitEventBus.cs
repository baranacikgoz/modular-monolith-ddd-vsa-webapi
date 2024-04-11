using Common.Core.Contracts;
using Common.Core.Interfaces;
using Common.EventBus.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Common.EventBus;

public partial class MassTransitEventBus(
    IBus bus,
    ILogger<MassTransitEventBus> logger
    ) : IEventBus
{
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent
    {
        var eventId = @event.EventId;
        var eventName = @event.GetType().Name;

        LogPublishingEvent(logger, eventName, eventId);

        await bus.Publish(@event, cancellationToken);

        LogPublishedEvent(logger, eventName, eventId);
    }

    [LoggerMessage(
        Level = LogLevel.Debug,
        Message = "Publishing {EventName} with the Id {Id} ...")]
    private static partial void LogPublishingEvent(ILogger logger, string eventName, Guid id);

    [LoggerMessage(
        Level = LogLevel.Debug,
        Message = "Published {EventName} with the Id {Id} successfully.")]
    private static partial void LogPublishedEvent(ILogger logger, string eventName, Guid id);
}
