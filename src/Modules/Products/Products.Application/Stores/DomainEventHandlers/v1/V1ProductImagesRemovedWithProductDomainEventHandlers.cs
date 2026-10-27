using Common.Application.EventBus;
using Common.IntegrationEvents;
using Products.Domain.Stores.DomainEvents.v1;

namespace Products.Application.Stores.DomainEventHandlers.v1;

public static class V1ProductImagesRemovedWithProductDomainEventHandlers
{
    /// <summary>The image rows are deleted in this save; the objects they pointed at are deleted by the consumer of this message.</summary>
    public class ProductImageObjectsOrphanedIntegrationEventPublishingHandler(IIntegrationEventOutbox outbox)
        : DomainEventHandlerBase<V1ProductImagesRemovedWithProductDomainEvent>
    {
        public override Task HandleAsync(V1ProductImagesRemovedWithProductDomainEvent @event, CancellationToken cancellationToken)
        {
            outbox.Collect(new ProductImageObjectsOrphanedIntegrationEvent(@event.ProductId.Value, @event.ObjectKeys));
            return Task.CompletedTask;
        }
    }
}
