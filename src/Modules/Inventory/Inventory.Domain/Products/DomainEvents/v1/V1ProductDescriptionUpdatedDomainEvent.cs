using Common.Domain.Events;

namespace Inventory.Domain.Products.DomainEvents.v1;
public sealed record V1ProductDescriptionUpdatedDomainEvent(ProductId Id, string Description) : DomainEvent;
