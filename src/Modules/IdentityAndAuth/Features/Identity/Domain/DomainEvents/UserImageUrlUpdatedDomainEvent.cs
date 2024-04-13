using Common.Core.Contracts;

namespace IdentityAndAuth.Features.Identity.Domain.DomainEvents;

public sealed record UserImageUrlUpdatedDomainEvent(ApplicationUserId UserId, Uri ImageUrl) : DomainEvent;
