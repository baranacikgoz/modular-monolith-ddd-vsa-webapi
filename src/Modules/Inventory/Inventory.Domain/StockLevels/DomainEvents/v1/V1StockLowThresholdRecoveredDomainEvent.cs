using Common.Domain.Events;

namespace Inventory.Domain.StockLevels.DomainEvents.v1;

/// <summary>
///     Raised once when the on-hand quantity goes from below the low stock threshold back to at-or-above it, by a
///     movement or by a threshold change.
/// </summary>
public sealed record V1StockLowThresholdRecoveredDomainEvent(
    StockLevelId StockLevelId,
    DefaultIdType ProductId,
    int QuantityOnHand,
    int LowStockThreshold
) : DomainEvent;
