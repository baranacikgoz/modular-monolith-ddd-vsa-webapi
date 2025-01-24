using Common.Domain.Events;

namespace Products.Domain.Products.DomainEvents.v1;

public sealed record V1ProductDescriptionUpdatedDomainEvent(ProductId ProductId, string Description) : DomainEvent;
