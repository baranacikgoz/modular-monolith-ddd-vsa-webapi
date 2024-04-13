using Common.Core.Contracts;

namespace Common.IntegrationEvents;
public sealed record StoreCreatedIntegrationEvent(Guid StoreId, Guid OwnerId) : DomainEvent;

public sealed record ProductCreatedIntegrationEvent(Guid ProductId, Guid StoreId, string Name) : DomainEvent;
