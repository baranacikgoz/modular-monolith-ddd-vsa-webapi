using Common.Domain.Events;

namespace Products.Domain.Products.DomainEvents.v1;

public sealed record V1ProductQuantityIncreasedDomainEvent(ProductId ProductId, int Quantity) : DomainEvent;
