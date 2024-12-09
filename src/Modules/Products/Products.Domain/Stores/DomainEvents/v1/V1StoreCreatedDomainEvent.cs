using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;

namespace Products.Domain.Stores.DomainEvents.v1;

public sealed record V1StoreCreatedDomainEvent(
    StoreId StoreId,
    ApplicationUserId OwnerId,
    string Name,
    string Description,
    Uri? LogoUrl) : DomainEvent;
