using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using Notifications.Application.Otp;
using Notifications.Infrastructure.Telemetry;

namespace Notifications.Infrastructure.InterModuleRequestHandlers;

public sealed class VerifyEmailOtpRequestHandler(IOtpService otpService)
    : InterModuleRequestHandler<VerifyEmailOtpRequest, VerifyEmailOtpResponse>
{
    public override async Task<VerifyEmailOtpResponse> HandleAsync(
        VerifyEmailOtpRequest request,
        CancellationToken cancellationToken)
    {
        var outcome = await otpService.VerifyThenRemoveAsync(
            request.Email,
            request.Otp,
            request.Purpose,
            request.ContextId,
            cancellationToken);

        NotificationsTelemetry.RecordOtpVerification(request.Purpose, outcome.ToString());

        return outcome switch
        {
            OtpVerificationOutcome.Success => new VerifyEmailOtpResponse(OtpVerificationFailureReason.None),
            OtpVerificationOutcome.TooManyAttempts => new VerifyEmailOtpResponse(OtpVerificationFailureReason
                .TooManyAttempts),
            OtpVerificationOutcome.InvalidOtp => new VerifyEmailOtpResponse(OtpVerificationFailureReason.InvalidOtp),
            _ => throw new ArgumentOutOfRangeException($"Unexpected OtpVerificationOutcome: {outcome}")
        };
    }
}
