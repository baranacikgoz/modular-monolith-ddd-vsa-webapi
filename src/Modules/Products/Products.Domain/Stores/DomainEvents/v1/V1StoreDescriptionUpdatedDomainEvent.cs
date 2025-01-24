using Common.Domain.Events;

namespace Products.Domain.Stores.DomainEvents.v1;

public sealed record V1StoreDescriptionUpdatedDomainEvent(StoreId StoreId, string Description) : DomainEvent;
