using Common.Core.Contracts;

namespace Sales.Features.Stores.Domain.DomainEvents;

public sealed record StoreCreatedDomainEvent(StoreId Id, Guid OwnerId, string Name) : DomainEvent;
