using Common.Core.Contracts;
using Common.Core.Contracts.Money;
using Sales.Features.Stores.Domain;

namespace Sales.Features.Products.Domain.DomainEvents;
public sealed record ProductCreatedDomainEvent(ProductId Id, StoreId StoreId, Price Price, string Name, string Description) : DomainEvent;
public sealed record ProductPriceIncreasedEvent(Price OldPrice, Price NewPrice) : DomainEvent;
public sealed record ProductPriceDecreasedEvent(Price OldPrice, Price NewPrice) : DomainEvent;
