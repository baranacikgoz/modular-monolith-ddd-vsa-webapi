using Common.Core.Contracts;
using Common.EventBus.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Common.EventBus;

public partial class MassTransitEventBus(
    IBus bus,
    ILogger<MassTransitEventBus> logger
    ) : IEventBus
{
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : DomainEvent
    {
        var eventName = @event.GetType().Name;

        LogPublishingEvent(logger, eventName);

        await bus.Publish(@event, cancellationToken);

        LogPublishedEvent(logger, eventName);
    }

    [LoggerMessage(
        Level = LogLevel.Debug,
        Message = "Publishing {EventName} event...")]
    private static partial void LogPublishingEvent(ILogger logger, string eventName);

    [LoggerMessage(
        Level = LogLevel.Debug,
        Message = "Published {EventName} event successfully.")]
    private static partial void LogPublishedEvent(ILogger logger, string eventName);
}
