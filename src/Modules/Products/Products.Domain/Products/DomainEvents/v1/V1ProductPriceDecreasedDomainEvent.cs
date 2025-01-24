using Common.Domain.Events;

namespace Products.Domain.Products.DomainEvents.v1;

public sealed record V1ProductPriceDecreasedDomainEvent(ProductId ProductId, decimal Price) : DomainEvent;
