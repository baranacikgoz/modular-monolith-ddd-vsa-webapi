using Common.Domain.Events;

namespace Products.Domain.Products.DomainEvents.v1;
public sealed record V1ProductNameUpdatedDomainEvent(ProductId Id, string Name) : DomainEvent;
