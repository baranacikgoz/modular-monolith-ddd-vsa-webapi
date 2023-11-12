using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Identity.Domain.Errors;

internal static class PhoneVerificationTokenErrors
{
    public static readonly Error VerificationFailed = new(nameof(VerificationFailed));
}
