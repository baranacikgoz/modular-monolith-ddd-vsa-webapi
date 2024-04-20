using Common.Core.Contracts;
using Common.Core.Contracts.Identity;

namespace IdentityAndAuth.Features.Identity.Domain.DomainEvents;

public sealed record RefreshTokenUpdatedDomainEvent(ApplicationUserId UserId) : DomainEvent;
