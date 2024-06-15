using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;

namespace IAM.Domain.Identity.DomainEvents.v1;

public sealed record V1UserImageUrlUpdatedDomainEvent(ApplicationUserId UserId, Uri ImageUrl) : DomainEvent;
