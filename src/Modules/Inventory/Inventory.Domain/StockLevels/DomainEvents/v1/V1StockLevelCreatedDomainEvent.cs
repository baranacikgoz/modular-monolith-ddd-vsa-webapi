using Common.Domain.Events;

namespace Inventory.Domain.StockLevels.DomainEvents.v1;

public sealed record V1StockLevelCreatedDomainEvent(
    StockLevelId StockLevelId,
    DefaultIdType ProductId,
    int QuantityOnHand) : DomainEvent;
