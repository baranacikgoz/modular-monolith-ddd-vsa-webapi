using Common.Domain.Events;
using Inventory.Domain.StoreProducts;

namespace Inventory.Domain.Stores.DomainEvents;

public sealed record ProductPriceIncreasedDomainEvent(StoreProduct Product, decimal NewPrice) : DomainEvent;
