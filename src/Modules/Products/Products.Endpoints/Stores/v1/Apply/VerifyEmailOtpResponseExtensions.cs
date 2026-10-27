using Common.Domain.ResultMonad;
using Common.InterModuleRequests.Notifications;
using Products.Domain.Stores.Errors;

namespace Products.Endpoints.Stores.v1.Apply;

internal static class VerifyEmailOtpResponseExtensions
{
    internal static Result ToResult(this VerifyEmailOtpResponse response)
    {
        return response.FailureReason switch
        {
            OtpVerificationFailureReason.None => Result.Success,
            OtpVerificationFailureReason.InvalidOtp => StoreErrors.InvalidEmailVerificationToken,
            OtpVerificationFailureReason.TooManyAttempts => StoreErrors.TooManyEmailVerificationAttempts,
            _ => throw new ArgumentOutOfRangeException(
                nameof(response), response.FailureReason, $"Unknown OtpVerificationFailureReason: {response.FailureReason}")
        };
    }
}
