namespace Common.IntegrationEvents;

/// <summary>A warehouse confirmed a stock reservation: its quantity must leave the product's on-hand stock.</summary>
public sealed record StockReservationCommittedIntegrationEvent(DefaultIdType ReservationId, DefaultIdType ProductId, int Quantity)
    : IntegrationEvent;

/// <summary>A product's on-hand quantity fell below its low stock threshold. Raised once per crossing, not per movement below.</summary>
public sealed record StockLowThresholdCrossedIntegrationEvent(DefaultIdType ProductId, int QuantityOnHand, int LowStockThreshold)
    : IntegrationEvent;

/// <summary>A product's on-hand quantity is back at or above its low stock threshold.</summary>
public sealed record StockLowThresholdRecoveredIntegrationEvent(DefaultIdType ProductId, int QuantityOnHand, int LowStockThreshold)
    : IntegrationEvent;

/// <summary>
///     Consumed within Inventory itself, used as a queue message: the stock of a store's products changed (or a
///     mark waited too long), so the warehouse of this connection should be sent the current quantities. Every quantity
///     change of a save collects one, and the events of one connection collected in one save are written once: the
///     consumer pushes whatever is dirty when it runs, so one message stands for all of them.
/// </summary>
public sealed record StockPushDueIntegrationEvent(DefaultIdType ConnectionId) : IntegrationEvent, ICoalescableIntegrationEvent
{
    string? ICoalescableIntegrationEvent.CoalesceKey => ConnectionId.ToString();
}
