using Common.Core.Contracts;
using Common.Core.Contracts.Identity;

namespace IdentityAndAuth.Features.Identity.Domain.DomainEvents;

public sealed record UserImageUrlUpdatedDomainEvent(ApplicationUserId UserId, Uri ImageUrl) : DomainEvent;
