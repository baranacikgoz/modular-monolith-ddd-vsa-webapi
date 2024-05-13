using Common.Domain.ResultMonad;

namespace IdentityAndAuth.Domain.Identity.Errors;

public static class OtpErrors
{
    public static readonly Error InvalidOtp = new() { Key = nameof(InvalidOtp) };
}
