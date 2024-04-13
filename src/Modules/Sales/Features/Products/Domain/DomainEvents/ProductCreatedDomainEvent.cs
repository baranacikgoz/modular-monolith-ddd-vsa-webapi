using Common.Core.Contracts;
using Sales.Features.Stores.Domain;

namespace Sales.Features.Products.Domain.DomainEvents;
public sealed record ProductCreatedDomainEvent(ProductId Id, StoreId StoreId, string Name, string Description) : DomainEvent;

