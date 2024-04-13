using Common.EventBus.Contracts;
using Common.IntegrationEvents;
using Sales.Features.Products.Domain.DomainEvents;

namespace Sales.Features.Products.Domain.DomainEventHandlers;
public class ProductCreatedDomainEventHandler(
    IEventBus eventBus
    ) : EventHandlerBase<ProductCreatedDomainEvent>
{
    protected override async Task HandleAsync(ProductCreatedDomainEvent @event, CancellationToken cancellationToken)
    {
        var integrationEvent = new ProductCreatedIntegrationEvent(@event.Id.Value, @event.StoreId.Value, @event.Name);

        await eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}
