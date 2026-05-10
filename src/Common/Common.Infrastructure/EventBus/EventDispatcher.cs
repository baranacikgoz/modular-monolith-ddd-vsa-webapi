using Common.Application.EventBus;
using Common.Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.EventBus;

public class EventDispatcher(IServiceProvider serviceProvider) : IEventDispatcher
{
    public async Task DispatchAsync(IEvent @event, CancellationToken cancellationToken)
    {
        var handlerType = typeof(IEventHandler<>).MakeGenericType(@event.GetType());
        var handlers = serviceProvider.GetServices(handlerType);
        foreach (var handler in handlers)
        {
            if (handler is IEventHandlerWrapper wrapper)
            {
                await wrapper.HandleAsync(@event, cancellationToken);
            }
        }
    }
}
