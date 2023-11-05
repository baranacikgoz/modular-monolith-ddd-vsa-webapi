using Common.Core.Contracts;
using MassTransit;

namespace Common.Eventbus;

public class MassTransitEventBus(IBusControl busControl) : IEventBus
{
    public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : DomainEvent
        => busControl.Publish(@event, cancellationToken);
}
