using Common.Domain.EventBus;
using Common.IntegrationEvents;
using Inventory.Domain.Stores.DomainEvents;

namespace Inventory.Application.Stores.DomainEventHandlers;
public class StoreCreatedDomainEventHandler(
    IEventBus eventBus
    ) : EventHandlerBase<StoreCreatedDomainEvent>
{
    protected override async Task HandleAsync(StoreCreatedDomainEvent @event, CancellationToken cancellationToken)
    {
        var integrationEvent = new StoreCreatedIntegrationEvent(@event.StoreId.Value, @event.OwnerId);

        await eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}
