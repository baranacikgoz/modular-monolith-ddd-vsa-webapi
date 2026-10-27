using Common.Domain.Events;

namespace Inventory.Domain.StockLevels.DomainEvents.v1;

public sealed record V1StockLowThresholdChangedDomainEvent(
    StockLevelId StockLevelId,
    DefaultIdType ProductId,
    int PreviousThreshold,
    int NewThreshold
) : DomainEvent;
