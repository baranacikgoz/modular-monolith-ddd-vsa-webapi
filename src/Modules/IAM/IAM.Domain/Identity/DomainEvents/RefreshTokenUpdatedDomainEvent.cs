using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;

namespace IAM.Domain.Identity.DomainEvents;

public sealed record RefreshTokenUpdatedDomainEvent(ApplicationUserId UserId) : DomainEvent;
