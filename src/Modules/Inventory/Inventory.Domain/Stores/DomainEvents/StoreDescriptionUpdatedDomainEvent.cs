using Common.Domain.Events;

namespace Inventory.Domain.Stores.DomainEvents;

public sealed record StoreDescriptionUpdatedDomainEvent(StoreId StoreId, string OldDescription, string NewDescription) : DomainEvent;
