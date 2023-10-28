using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Users.Services.PhoneVerificationToken.Errors;

public static class PhoneVerificationTokenErrors
{
    public static readonly Error VerificationFailed = new(nameof(VerificationFailed));
}
