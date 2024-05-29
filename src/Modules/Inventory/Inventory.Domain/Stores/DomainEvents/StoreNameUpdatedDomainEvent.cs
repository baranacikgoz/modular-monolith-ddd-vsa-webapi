using Common.Domain.Events;

namespace Inventory.Domain.Stores.DomainEvents;

public sealed record StoreNameUpdatedDomainEvent(StoreId StoreId, string OldName, string NewName) : DomainEvent;
