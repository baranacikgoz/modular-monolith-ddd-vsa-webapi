using Common.Core.Contracts;

namespace IdentityAndAuth.Features.Identity.Domain.DomainEvents;

public sealed record RefreshTokenUpdatedDomainEvent(ApplicationUserId UserId) : DomainEvent;
