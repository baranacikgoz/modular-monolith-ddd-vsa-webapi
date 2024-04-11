using Common.Core.Contracts;

namespace Common.Events;

public sealed record StoreCreatedDomainEvent(Guid Id, Guid OwnerId, string Name) : DomainEvent;
public sealed record ProductAddedDomainEvent(Guid StoreId, string Name, string Description) : DomainEvent;
public sealed record ProductRemovedDomainEvent(Guid StoreId, Guid ProductId) : DomainEvent;
