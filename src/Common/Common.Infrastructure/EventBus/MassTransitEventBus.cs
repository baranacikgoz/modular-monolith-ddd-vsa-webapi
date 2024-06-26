using Common.Application.EventBus;
using Common.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Common.Infrastructure.EventBus;

public partial class MassTransitEventBus(
    IBus bus,
    ILogger<MassTransitEventBus> logger
    ) : IEventBus
{
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent
    {
        var eventName = @event.GetType().Name;

        LogPublishingEvent(logger, eventName);

        await bus.Publish(@event, cancellationToken);

        LogPublishedEvent(logger, eventName);
    }

    [LoggerMessage(
        Level = LogLevel.Debug,
        Message = "Publishing {EventName} ...")]
    private static partial void LogPublishingEvent(ILogger logger, string eventName);

    [LoggerMessage(
        Level = LogLevel.Debug,
        Message = "Published {EventName} successfully.")]
    private static partial void LogPublishedEvent(ILogger logger, string eventName);
}
