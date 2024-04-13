using Common.Core.Contracts;

namespace IdentityAndAuth.Features.Identity.Domain.DomainEvents;

public sealed record RefreshTokenUpdatedDomainEvent(Guid UserId) : DomainEvent;
