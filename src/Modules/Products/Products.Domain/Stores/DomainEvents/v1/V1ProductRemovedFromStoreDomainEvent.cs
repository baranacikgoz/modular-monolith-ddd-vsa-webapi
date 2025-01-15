using Common.Domain.Events;
using Products.Domain.StoreProducts;

namespace Products.Domain.Stores.DomainEvents.v1;
public sealed record V1ProductRemovedFromStoreDomainEvent(StoreId StoreId, StoreProduct Product) : DomainEvent;