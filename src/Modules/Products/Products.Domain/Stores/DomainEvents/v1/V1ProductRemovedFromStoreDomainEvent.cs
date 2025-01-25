using Common.Domain.Events;
using Products.Domain.Products;

namespace Products.Domain.Stores.DomainEvents.v1;
public sealed record V1ProductRemovedFromStoreDomainEvent(StoreId StoreId, Product Product) : DomainEvent;
