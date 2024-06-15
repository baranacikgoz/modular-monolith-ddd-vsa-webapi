using Common.Domain.ResultMonad;

namespace IAM.Domain.Identity.Errors;

public static class PhoneVerificationTokenErrors
{
    public static readonly Error PhoneVerificationTokensNotMatching = new() { Key = nameof(PhoneVerificationTokensNotMatching) };
}
