using Common.Domain.Events;

namespace Products.Domain.Products.DomainEvents.v1;
public sealed record V1ProductCreatedDomainEvent(ProductId Id, string Name, string Description) : DomainEvent;
