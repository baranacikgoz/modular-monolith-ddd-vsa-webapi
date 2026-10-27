using Common.Application.EventBus;
using Common.IntegrationEvents;
using Products.Domain.Stores.DomainEvents.v1;

namespace Products.Application.Stores.DomainEventHandlers.v1;

public static class V1StoreApprovedDomainEventHandlers
{
    /// <summary>IAM grants the store-owner role from this event; the Products module never asks IAM for it synchronously.</summary>
    public class StoreApprovedIntegrationEventPublishingHandler(IIntegrationEventOutbox outbox)
        : DomainEventHandlerBase<V1StoreApprovedDomainEvent>
    {
        public override Task HandleAsync(V1StoreApprovedDomainEvent @event, CancellationToken cancellationToken)
        {
            outbox.Collect(new StoreApprovedIntegrationEvent(@event.StoreId.Value, @event.OwnerId));
            return Task.CompletedTask;
        }
    }
}
