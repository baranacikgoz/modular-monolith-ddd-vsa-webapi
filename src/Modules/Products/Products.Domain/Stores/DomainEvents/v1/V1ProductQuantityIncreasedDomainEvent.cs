using Common.Domain.Events;
using Products.Domain.StoreProducts;

namespace Products.Domain.Stores.DomainEvents.v1;

public sealed record V1ProductQuantityIncreasedDomainEvent(StoreProduct Product, int NewQuantity) : DomainEvent;
