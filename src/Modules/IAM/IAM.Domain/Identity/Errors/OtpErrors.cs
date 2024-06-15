using Common.Domain.ResultMonad;

namespace IAM.Domain.Identity.Errors;

public static class OtpErrors
{
    public static readonly Error InvalidOtp = new() { Key = nameof(InvalidOtp) };
}
