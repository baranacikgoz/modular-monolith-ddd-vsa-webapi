using Common.Application.EventBus;
using Common.Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.EventBus;

public class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher
{
    public async Task DispatchAsync(DomainEvent @event, CancellationToken cancellationToken)
    {
        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());
        var handlers = serviceProvider.GetServices(handlerType);
        foreach (var handler in handlers)
        {
            if (handler is IDomainEventHandlerWrapper wrapper)
            {
                await wrapper.HandleAsync(@event, cancellationToken);
            }
        }
    }
}
