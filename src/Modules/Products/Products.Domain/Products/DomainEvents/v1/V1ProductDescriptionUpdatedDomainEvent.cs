using Common.Domain.Events;

namespace Products.Domain.Products.DomainEvents.v1;
public sealed record V1ProductDescriptionUpdatedDomainEvent(ProductId Id, string Description) : DomainEvent;
