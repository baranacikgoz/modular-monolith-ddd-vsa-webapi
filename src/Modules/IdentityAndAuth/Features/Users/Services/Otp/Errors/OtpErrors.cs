using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Users.Services.Otp.Errors;

public static class OtpErrors
{
    public static readonly Error InvalidOtp = new(nameof(InvalidOtp));
}
