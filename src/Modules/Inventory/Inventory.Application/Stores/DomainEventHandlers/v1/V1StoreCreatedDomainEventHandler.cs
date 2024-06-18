using Common.Application.EventBus;
using Common.IntegrationEvents;
using Inventory.Domain.Stores.DomainEvents.v1;

namespace Inventory.Application.Stores.DomainEventHandlers.v1;
public class V1StoreCreatedDomainEventHandler(
    IEventBus eventBus
    ) : EventHandlerBase<V1StoreCreatedDomainEvent>
{
    protected override async Task HandleAsync(V1StoreCreatedDomainEvent @event, CancellationToken cancellationToken)
    {
        var integrationEvent = new StoreCreatedIntegrationEvent(@event.StoreId.Value, @event.OwnerId);

        await eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}
