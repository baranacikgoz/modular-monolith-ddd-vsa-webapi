using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using Notifications.Application.Otp;
using Notifications.Infrastructure.Telemetry;

namespace Notifications.Infrastructure.InterModuleRequestHandlers;

public sealed class VerifyPhoneOtpRequestHandler(IOtpService otpService)
    : InterModuleRequestHandler<VerifyPhoneOtpRequest, VerifyPhoneOtpResponse>
{
    public override async Task<VerifyPhoneOtpResponse> HandleAsync(
        VerifyPhoneOtpRequest request,
        CancellationToken cancellationToken)
    {
        var outcome = await otpService.VerifyThenRemoveAsync(
            request.PhoneNumber,
            request.Otp,
            request.Purpose,
            request.ContextId,
            cancellationToken);

        NotificationsTelemetry.RecordOtpVerification(request.Purpose, outcome.ToString());

        return outcome switch
        {
            OtpVerificationOutcome.Success => new VerifyPhoneOtpResponse(OtpVerificationFailureReason.None),
            OtpVerificationOutcome.TooManyAttempts => new VerifyPhoneOtpResponse(OtpVerificationFailureReason
                .TooManyAttempts),
            OtpVerificationOutcome.InvalidOtp => new VerifyPhoneOtpResponse(OtpVerificationFailureReason.InvalidOtp),
            _ => throw new ArgumentOutOfRangeException($"Unexpected OtpVerificationOutcome: {outcome}")
        };
    }
}
