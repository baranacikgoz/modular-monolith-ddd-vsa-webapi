using Common.Domain.ResultMonad;

namespace IdentityAndAuth.Domain.Identity.Errors;

public static class PhoneVerificationTokenErrors
{
    public static readonly Error PhoneVerificationTokensNotMatching = new() { Key = nameof(PhoneVerificationTokensNotMatching) };
}
