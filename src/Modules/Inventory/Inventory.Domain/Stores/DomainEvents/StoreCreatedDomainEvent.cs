using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;

namespace Inventory.Domain.Stores.DomainEvents;

public sealed record StoreCreatedDomainEvent(
    StoreId StoreId,
    ApplicationUserId OwnerId,
    string Name,
    string Description,
    Uri? LogoUrl) : DomainEvent;
