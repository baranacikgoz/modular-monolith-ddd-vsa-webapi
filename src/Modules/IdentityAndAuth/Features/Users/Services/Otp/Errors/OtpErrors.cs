using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Users.Services.Otp.Errors;

internal static class OtpErrors
{
    public static readonly Error InvalidOtp = new(nameof(InvalidOtp));
}
