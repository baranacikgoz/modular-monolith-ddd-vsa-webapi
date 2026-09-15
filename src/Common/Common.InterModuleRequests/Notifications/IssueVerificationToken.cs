using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.Notifications;

/// <summary>
///     Mints an opaque, single-use verification token and stores it in the OTP store under
///     <paramref name="Purpose" />, exactly like an OTP code: a later <see cref="VerifyEmailOtpRequest" /> (or
///     <see cref="VerifyPhoneOtpRequest" />) for the same <paramref name="Identifier" /> and purpose consumes it.
///     Channel-agnostic: <paramref name="Identifier" /> is whatever the caller already verified ownership of
///     (a normalized email today; a phone number if this ever needs to be more than a one-time proof-of-OTP).
/// </summary>
public sealed record IssueVerificationTokenRequest(
    string Identifier,
    string Purpose
) : IInterModuleRequest<IssueVerificationTokenResponse>;

public sealed record IssueVerificationTokenResponse(string Token);
