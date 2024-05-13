using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;

namespace IdentityAndAuth.Domain.Identity.DomainEvents;

public sealed record UserImageUrlUpdatedDomainEvent(ApplicationUserId UserId, Uri ImageUrl) : DomainEvent;
