using Common.Domain.Events;
using Inventory.Domain.StoreProducts;

namespace Inventory.Domain.Stores.DomainEvents;

public sealed record ProductAddedToStoreDomainEvent(StoreId StoreId, StoreProduct Product) : DomainEvent;
