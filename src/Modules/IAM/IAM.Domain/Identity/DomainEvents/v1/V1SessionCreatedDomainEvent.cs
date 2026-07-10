using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;
using IAM.Domain.Identity.Sessions;

namespace IAM.Domain.Identity.DomainEvents.v1;

public sealed record V1SessionCreatedDomainEvent(
    ApplicationUserId UserId,
    SessionId SessionId,
    Guid DeviceId,
    string ClientId,
    string? DeviceName
) : DomainEvent;
