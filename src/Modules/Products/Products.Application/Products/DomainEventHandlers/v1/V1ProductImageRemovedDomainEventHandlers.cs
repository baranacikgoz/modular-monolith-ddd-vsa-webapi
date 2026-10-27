using Common.Application.EventBus;
using Common.IntegrationEvents;
using Products.Domain.Products.DomainEvents.v1;

namespace Products.Application.Products.DomainEventHandlers.v1;

public static class V1ProductImageRemovedDomainEventHandlers
{
    /// <summary>The image row is deleted in this save; the object it pointed at is deleted by the consumer of this message.</summary>
    public class RemovedImageObjectOrphanedIntegrationEventPublishingHandler(IIntegrationEventOutbox outbox)
        : DomainEventHandlerBase<V1ProductImageRemovedDomainEvent>
    {
        public override Task HandleAsync(V1ProductImageRemovedDomainEvent @event, CancellationToken cancellationToken)
        {
            outbox.Collect(new ProductImageObjectsOrphanedIntegrationEvent(@event.ProductId.Value, [@event.ObjectKey]));
            return Task.CompletedTask;
        }
    }
}
