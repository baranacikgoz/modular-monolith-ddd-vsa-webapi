using Common.Domain.Events;
using Inventory.Domain.StoreProducts;

namespace Inventory.Domain.Stores.DomainEvents;

public sealed record ProductPriceDecreasedDomainEvent(StoreProduct Product, decimal NewPrice) : DomainEvent;
