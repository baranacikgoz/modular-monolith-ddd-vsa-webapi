using Common.Domain.EventBus;
using Common.IntegrationEvents;
using Inventory.Domain.Products.DomainEvents;

namespace Inventory.Application.Products.DomainEventHandlers;
public class ProductCreatedDomainEventHandler(
    IEventBus eventBus
    ) : EventHandlerBase<ProductCreatedDomainEvent>
{
    protected override async Task HandleAsync(ProductCreatedDomainEvent @event, CancellationToken cancellationToken)
    {
        var integrationEvent = new ProductCreatedIntegrationEvent(@event.Id.Value, @event.Name, @event.Description);

        await eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}
