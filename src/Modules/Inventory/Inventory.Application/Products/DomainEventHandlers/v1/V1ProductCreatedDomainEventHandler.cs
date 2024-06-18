using Common.Application.EventBus;
using Common.IntegrationEvents;
using Inventory.Domain.Products.DomainEvents.v1;

namespace Inventory.Application.Products.DomainEventHandlers.v1;
public class V1ProductCreatedDomainEventHandler(
    IEventBus eventBus
    ) : EventHandlerBase<V1ProductCreatedDomainEvent>
{
    protected override async Task HandleAsync(V1ProductCreatedDomainEvent @event, CancellationToken cancellationToken)
    {
        var integrationEvent = new ProductCreatedIntegrationEvent(@event.Id.Value, @event.Name, @event.Description);

        await eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}
