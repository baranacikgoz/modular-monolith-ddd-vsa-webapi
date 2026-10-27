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
