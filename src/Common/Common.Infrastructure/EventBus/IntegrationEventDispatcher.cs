using Common.Application.EventBus;
using Common.IntegrationEvents;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.EventBus;

public class IntegrationEventDispatcher(IServiceProvider serviceProvider) : IIntegrationEventDispatcher
{
    public async Task DispatchAsync(IntegrationEvent @event, CancellationToken cancellationToken)
    {
        var handlerType = typeof(IIntegrationEventHandler<>).MakeGenericType(@event.GetType());
        var handlers = serviceProvider.GetServices(handlerType);
        foreach (var handler in handlers)
        {
            if (handler is IIntegrationEventHandlerWrapper wrapper)
                await wrapper.HandleAsync(@event, cancellationToken);
        }
    }
}
