using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;

namespace IAM.Domain.Identity.DomainEvents.v1;

public sealed record V1RefreshTokenRevokedDomainEvent(ApplicationUserId UserId) : DomainEvent;
