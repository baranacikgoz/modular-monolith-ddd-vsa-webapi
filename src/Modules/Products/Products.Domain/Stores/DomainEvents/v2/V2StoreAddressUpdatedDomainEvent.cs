using Common.Domain.Events;

namespace Products.Domain.Stores.DomainEvents.v2;

/// <summary>V1 plus the address the store had before the change, so a reader of the history sees the transition without
/// replaying the rows before it. V1 stays in the tree for its AuditLog rows.</summary>
public sealed record V2StoreAddressUpdatedDomainEvent(StoreId StoreId, string Address, string PreviousAddress) : DomainEvent;
