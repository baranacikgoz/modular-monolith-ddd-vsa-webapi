using Common.Domain.Events;
using Inventory.Domain.StoreProducts;

namespace Inventory.Domain.Stores.DomainEvents;

public sealed record ProductQuantityDecreasedDomainEvent(StoreProduct Product, int NewQuantity) : DomainEvent;
