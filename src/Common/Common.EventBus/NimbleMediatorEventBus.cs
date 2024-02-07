using Common.Core.Contracts;
using Common.EventBus.Contracts;
using Microsoft.Extensions.Logging;
using NimbleMediator;

namespace Common.EventBus;

public partial class NimbleMediatorEventBus(
    IPublisher publisher,
    ILogger<NimbleMediatorEventBus> logger
    ) : IEventBus
{
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : DomainEvent
    {
        try
        {
            await publisher.PublishAsync(@event, cancellationToken);
            LogEventPublished(logger, @event.GetType().Name);
        }
#pragma warning disable CA1031
        catch (Exception e)
        {
            LogOneOrMoreEventHandlersFailed(logger, @event.GetType().Name, e.Message);
        }
#pragma warning restore CA1031
    }

    [LoggerMessage(
        Level = LogLevel.Critical,
        Message = "One or more event handlers failed for event {EventName} with message {Message}")]
    private static partial void LogOneOrMoreEventHandlersFailed(ILogger logger, string eventName, string message);

    [LoggerMessage(
        Level = LogLevel.Debug,
        Message = "Event {EventName} published successfully")]
    private static partial void LogEventPublished(ILogger logger, string eventName);
}
