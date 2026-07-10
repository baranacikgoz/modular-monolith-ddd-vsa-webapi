using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;
using IAM.Domain.Identity.Sessions;

namespace IAM.Domain.Identity.DomainEvents.v1;

// Marker only — never carries TokenHash. Events are persisted (AuditLog); tokens must not be there unprotected.
public sealed record V1SessionRefreshedDomainEvent(ApplicationUserId UserId, SessionId SessionId) : DomainEvent;
