using Common.Domain.Events;

namespace Products.Domain.Stores.DomainEvents.v1;

public sealed record V1StoreAddressUpdatedDomainEvent(StoreId StoreId, string Address) : DomainEvent;
