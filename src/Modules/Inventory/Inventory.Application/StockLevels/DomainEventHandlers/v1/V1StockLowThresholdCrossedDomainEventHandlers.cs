using Common.Application.EventBus;
using Common.IntegrationEvents;
using Inventory.Domain.StockLevels.DomainEvents.v1;

namespace Inventory.Application.StockLevels.DomainEventHandlers.v1;

public static class V1StockLowThresholdCrossedDomainEventHandlers
{
    public class StockLowThresholdCrossedIntegrationEventPublishingHandler(IIntegrationEventOutbox outbox)
        : DomainEventHandlerBase<V1StockLowThresholdCrossedDomainEvent>
    {
        public override Task HandleAsync(V1StockLowThresholdCrossedDomainEvent @event, CancellationToken cancellationToken)
        {
            outbox.Collect(new StockLowThresholdCrossedIntegrationEvent(@event.ProductId, @event.QuantityOnHand, @event.LowStockThreshold));
            return Task.CompletedTask;
        }
    }
}
