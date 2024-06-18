using Common.Application.EventBus;
using Common.Domain.Events;
using Microsoft.Extensions.Logging;
using NimbleMediator;

namespace Common.Infrastructure.EventBus;

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
            LogPublishingEvent(logger, @event.GetType().Name);
            await publisher.PublishAsync(@event, cancellationToken);
            LogPublishedEvent(logger, @event.GetType().Name);
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
        Message = "One or more event handlers failed for event {EventName} with the message {Message}")]
    private static partial void LogOneOrMoreEventHandlersFailed(ILogger logger, string eventName, string message);

    [LoggerMessage(
        Level = LogLevel.Debug,
        Message = "Publishing {EventName} ...")]
    private static partial void LogPublishingEvent(ILogger logger, string eventName);

    [LoggerMessage(
        Level = LogLevel.Debug,
        Message = "Published {EventName} successfully.")]
    private static partial void LogPublishedEvent(ILogger logger, string eventName);
}
