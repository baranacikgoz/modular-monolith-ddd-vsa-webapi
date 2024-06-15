using Common.Domain.Events;

namespace Inventory.Domain.Stores.DomainEvents.v1;

public sealed record V1StoreNameUpdatedDomainEvent(StoreId StoreId, string OldName, string NewName) : DomainEvent;
