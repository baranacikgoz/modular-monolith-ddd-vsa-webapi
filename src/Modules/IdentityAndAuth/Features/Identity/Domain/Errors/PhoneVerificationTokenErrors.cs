using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Identity.Domain.Errors;

internal static class PhoneVerificationTokenErrors
{
    public static readonly Error TokenNotFound = new(nameof(TokenNotFound));
    public static readonly Error NotMatching = new(nameof(NotMatching));
}
