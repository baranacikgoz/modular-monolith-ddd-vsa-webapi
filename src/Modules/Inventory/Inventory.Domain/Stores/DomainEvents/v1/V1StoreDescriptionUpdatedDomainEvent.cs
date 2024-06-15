using Common.Domain.Events;

namespace Inventory.Domain.Stores.DomainEvents.v1;

public sealed record V1StoreDescriptionUpdatedDomainEvent(StoreId StoreId, string OldDescription, string NewDescription) : DomainEvent;
