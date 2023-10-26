using Common.Core.Contracts.Errors;

namespace IdentityAndAuth.Features.Users.Domain.Errors;

public sealed record UserNotFoundError() : DomainError;
