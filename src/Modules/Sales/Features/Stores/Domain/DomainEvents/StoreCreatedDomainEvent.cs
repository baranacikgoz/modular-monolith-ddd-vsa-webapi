using Common.Core.Contracts;
using Common.Core.Contracts.Identity;

namespace Sales.Features.Stores.Domain.DomainEvents;

public sealed record StoreCreatedDomainEvent(StoreId Id, ApplicationUserId OwnerId, string Name) : DomainEvent;
