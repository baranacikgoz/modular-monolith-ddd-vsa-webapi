using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;

namespace Products.Domain.Stores.DomainEvents.v1;

public sealed record V1StoreApprovedDomainEvent(StoreId StoreId, ApplicationUserId OwnerId) : DomainEvent;
