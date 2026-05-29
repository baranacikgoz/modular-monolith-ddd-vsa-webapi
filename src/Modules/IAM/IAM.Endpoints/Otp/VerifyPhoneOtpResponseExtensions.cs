using Common.Domain.ResultMonad;
using Common.InterModuleRequests.Notifications;
using IAM.Domain.Errors;

namespace IAM.Endpoints.Otp;

internal static class VerifyPhoneOtpResponseExtensions
{
    internal static Result ToResult(this VerifyPhoneOtpResponse response) =>
        response.FailureReason switch
        {
            OtpVerificationFailureReason.None => Result.Success,
            OtpVerificationFailureReason.TooManyAttempts => OtpErrors.TooManyFailedAttempts,
            OtpVerificationFailureReason.InvalidOtp => OtpErrors.InvalidOtp,
            _ => throw new ArgumentOutOfRangeException(
                nameof(response),
                response.FailureReason,
                $"Unknown OtpVerificationFailureReason: {response.FailureReason}")
        };
}
