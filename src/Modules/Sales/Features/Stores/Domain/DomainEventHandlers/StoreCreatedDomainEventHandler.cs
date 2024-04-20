using Common.EventBus.Contracts;
using Common.IntegrationEvents;
using Sales.Features.Stores.Domain.DomainEvents;

namespace Sales.Features.Stores.Domain.DomainEventHandlers;
public class StoreCreatedDomainEventHandler(
    IEventBus eventBus
    ) : EventHandlerBase<StoreCreatedDomainEvent>
{
    protected override async Task HandleAsync(StoreCreatedDomainEvent @event, CancellationToken cancellationToken)
    {
        var integrationEvent = new StoreCreatedIntegrationEvent(@event.Id.Value, @event.OwnerId.Value);

        await eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}
