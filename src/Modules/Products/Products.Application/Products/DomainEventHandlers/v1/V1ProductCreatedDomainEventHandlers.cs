using Common.Application.EventBus;
using Common.IntegrationEvents;
using Products.Domain.Products.DomainEvents.v1;

namespace Products.Application.Products.DomainEventHandlers.v1;

public static class V1ProductCreatedDomainEventHandlers
{
    public class ProductCreatedIntegrationEventPublishingHandler(IIntegrationEventOutbox outbox)
        : DomainEventHandlerBase<V1ProductCreatedDomainEvent>
    {
        public override Task HandleAsync(V1ProductCreatedDomainEvent @event, CancellationToken cancellationToken)
        {
            outbox.Collect(new ProductCreatedIntegrationEvent(@event.ProductId.Value, @event.Name, @event.Description, @event.Quantity));
            return Task.CompletedTask;
        }
    }
}
