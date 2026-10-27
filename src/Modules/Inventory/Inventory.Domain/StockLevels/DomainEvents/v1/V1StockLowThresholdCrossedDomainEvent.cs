using Common.Domain.Events;

namespace Inventory.Domain.StockLevels.DomainEvents.v1;

/// <summary>
///     Raised once when the on-hand quantity goes from at-or-above the low stock threshold to below it, by a movement
///     or by a threshold change; staying below raises nothing more.
/// </summary>
public sealed record V1StockLowThresholdCrossedDomainEvent(
    StockLevelId StockLevelId,
    DefaultIdType ProductId,
    int QuantityOnHand,
    int LowStockThreshold
) : DomainEvent;
