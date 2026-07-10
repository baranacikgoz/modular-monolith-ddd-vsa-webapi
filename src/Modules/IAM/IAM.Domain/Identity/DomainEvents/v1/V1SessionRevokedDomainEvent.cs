using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;
using IAM.Domain.Identity.Sessions;

namespace IAM.Domain.Identity.DomainEvents.v1;

public sealed record V1SessionRevokedDomainEvent(
    ApplicationUserId UserId,
    SessionId SessionId,
    SessionRevokedReason Reason
) : DomainEvent;
