using Common.Domain.Events;
using Inventory.Domain.StoreProducts;

namespace Inventory.Domain.Stores.DomainEvents.v1;

public sealed record V1ProductPriceIncreasedDomainEvent(StoreProduct Product, decimal NewPrice) : DomainEvent;
