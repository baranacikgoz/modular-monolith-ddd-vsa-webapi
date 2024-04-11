using Common.Core.Contracts;
using Common.Core.Interfaces;
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
        where TEvent : IEvent
{
        try
        {
            LogPublishingEvent(logger, @event.GetType().Name, @event.EventId);
            await publisher.PublishAsync(@event, cancellationToken);
            LogPublishedEvent(logger, @event.GetType().Name, @event.EventId);
        }
#pragma warning disable CA1031
        catch (Exception e)
        {
            LogOneOrMoreEventHandlersFailed(logger, @event.GetType().Name, @event.EventId, e.Message);
        }
#pragma warning restore CA1031
    }

    [LoggerMessage(
        Level = LogLevel.Critical,
        Message = "One or more event handlers failed for event {EventName} with Id {Id} and the message {Message}")]
    private static partial void LogOneOrMoreEventHandlersFailed(ILogger logger, string eventName, Guid id, string message);

    [LoggerMessage(
        Level = LogLevel.Debug,
        Message = "Publishing {EventName} with the Id {Id} ...")]
    private static partial void LogPublishingEvent(ILogger logger, string eventName, Guid id);

    [LoggerMessage(
        Level = LogLevel.Debug,
        Message = "Published {EventName} with the Id {Id} successfully.")]
    private static partial void LogPublishedEvent(ILogger logger, string eventName, Guid id);
}
