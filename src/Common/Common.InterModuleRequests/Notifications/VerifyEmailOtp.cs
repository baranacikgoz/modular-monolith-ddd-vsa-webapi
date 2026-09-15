using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.Notifications;

/// <summary><paramref name="Email" /> must already be trimmed and lowercased by the caller.
/// Reuses <see cref="OtpVerificationFailureReason"/>: same outcomes as the phone channel.</summary>
public sealed record VerifyEmailOtpRequest(
    string Email,
    string Otp,
    string Purpose,
    string? ContextId = null
) : IInterModuleRequest<VerifyEmailOtpResponse>;

public sealed record VerifyEmailOtpResponse(OtpVerificationFailureReason FailureReason);
