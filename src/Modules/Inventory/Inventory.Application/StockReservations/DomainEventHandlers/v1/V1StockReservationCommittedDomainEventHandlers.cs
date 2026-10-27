using Common.Application.EventBus;
using Common.IntegrationEvents;
using Inventory.Domain.StockReservations.DomainEvents.v1;

namespace Inventory.Application.StockReservations.DomainEventHandlers.v1;

public static class V1StockReservationCommittedDomainEventHandlers
{
    public class StockReservationCommittedIntegrationEventPublishingHandler(IIntegrationEventOutbox outbox)
        : DomainEventHandlerBase<V1StockReservationCommittedDomainEvent>
    {
        public override Task HandleAsync(V1StockReservationCommittedDomainEvent @event, CancellationToken cancellationToken)
        {
            outbox.Collect(new StockReservationCommittedIntegrationEvent(@event.StockReservationId.Value, @event.ProductId, @event.Quantity));
            return Task.CompletedTask;
        }
    }
}
