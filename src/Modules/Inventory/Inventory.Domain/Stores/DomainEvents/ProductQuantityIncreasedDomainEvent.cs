using Common.Domain.Events;
using Inventory.Domain.StoreProducts;

namespace Inventory.Domain.Stores.DomainEvents;

public sealed record ProductQuantityIncreasedDomainEvent(StoreProduct Product, int NewQuantity) : DomainEvent;
