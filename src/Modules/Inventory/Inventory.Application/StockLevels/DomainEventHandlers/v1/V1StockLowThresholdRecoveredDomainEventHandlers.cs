using Common.Application.EventBus;
using Common.IntegrationEvents;
using Inventory.Domain.StockLevels.DomainEvents.v1;

namespace Inventory.Application.StockLevels.DomainEventHandlers.v1;

public static class V1StockLowThresholdRecoveredDomainEventHandlers
{
    public class StockLowThresholdRecoveredIntegrationEventPublishingHandler(IIntegrationEventOutbox outbox)
        : DomainEventHandlerBase<V1StockLowThresholdRecoveredDomainEvent>
    {
        public override Task HandleAsync(V1StockLowThresholdRecoveredDomainEvent @event, CancellationToken cancellationToken)
        {
            outbox.Collect(new StockLowThresholdRecoveredIntegrationEvent(@event.ProductId, @event.QuantityOnHand, @event.LowStockThreshold));
            return Task.CompletedTask;
        }
    }
}
