using Common.Core.Contracts;

namespace IdentityAndAuth.Features.Identity.Domain.DomainEvents;

public sealed record UserImageUrlUpdatedDomainEvent(Guid UserId, Uri ImageUrl) : DomainEvent;
