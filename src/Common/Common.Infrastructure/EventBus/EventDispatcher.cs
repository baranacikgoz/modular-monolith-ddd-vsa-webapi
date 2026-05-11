using System.Diagnostics;
using Common.Application.EventBus;
using Common.Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Common.Infrastructure.EventBus;

public partial class EventDispatcher(
    IServiceProvider serviceProvider,
    ILogger<EventDispatcher> logger) : IEventDispatcher
{
    internal static readonly ActivitySource ActivitySource = new("ModularMonolith.EventBus");

    public async Task DispatchAsync(IEvent @event, CancellationToken cancellationToken)
    {
        using var activity = ActivitySource.StartActivity(
            $"Dispatch.{@event.GetType().Name}",
            ActivityKind.Internal);

        LogDispatching(logger, @event.GetType().Name);

        var handlerType = typeof(IEventHandler<>).MakeGenericType(@event.GetType());
        var handlers = serviceProvider.GetServices(handlerType);
        foreach (var handler in handlers)
        {
            if (handler is IEventHandlerWrapper wrapper)
            {
                await wrapper.HandleAsync(@event, cancellationToken);
            }
        }

        LogDispatched(logger, @event.GetType().Name);

        activity?.SetStatus(ActivityStatusCode.Ok);
    }

    [LoggerMessage(Level = LogLevel.Debug, Message = "Dispatching event {EventType}...")]
    private static partial void LogDispatching(ILogger logger, string eventType);

    [LoggerMessage(Level = LogLevel.Debug, Message = "Dispatched event {EventType}.")]
    private static partial void LogDispatched(ILogger logger, string eventType);
}
