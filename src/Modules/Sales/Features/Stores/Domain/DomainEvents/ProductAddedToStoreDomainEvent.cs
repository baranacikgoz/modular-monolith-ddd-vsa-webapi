using Common.Core.Contracts;
using Sales.Features.Products.Domain;

namespace Sales.Features.Stores.Domain.DomainEvents;

public sealed record ProductAddedToStoreDomainEvent(Store Store, Product Product) : DomainEvent;
