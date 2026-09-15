using Common.Domain.ResultMonad;
using Common.InterModuleRequests.Notifications;
using IAM.Domain.Errors;

namespace IAM.Endpoints.Otp;

internal static class VerifyEmailOtpResponseExtensions
{
    internal static Result ToResult(this VerifyEmailOtpResponse response) =>
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
