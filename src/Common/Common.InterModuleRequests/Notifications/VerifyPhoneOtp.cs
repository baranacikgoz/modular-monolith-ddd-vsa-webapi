using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.Notifications;

public enum OtpVerificationFailureReason { None, InvalidOtp, TooManyAttempts }

public sealed record VerifyPhoneOtpRequest(
    string PhoneNumber,
    string Otp,
    string Purpose,
    string? ContextId = null
) : IInterModuleRequest<VerifyPhoneOtpResponse>;

public sealed record VerifyPhoneOtpResponse(OtpVerificationFailureReason FailureReason);
