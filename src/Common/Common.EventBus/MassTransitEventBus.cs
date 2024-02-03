using Common.Core.Contracts;
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
        await bus.Publish(@event, cancellationToken);
        LogEventPublished(logger, @event.GetType().Name);
    }

    [LoggerMessage(
        Level = LogLevel.Debug,
        Message = "Event {EventName} published successfully.")]
    private static partial void LogEventPublished(ILogger logger, string eventName);
}
