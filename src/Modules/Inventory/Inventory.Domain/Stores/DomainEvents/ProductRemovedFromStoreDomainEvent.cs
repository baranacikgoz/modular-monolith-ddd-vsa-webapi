using Common.Domain.Events;
using Inventory.Domain.StoreProducts;

namespace Inventory.Domain.Stores.DomainEvents;
public sealed record ProductRemovedFromStoreDomainEvent(StoreId StoreId, StoreProduct Product) : DomainEvent;
