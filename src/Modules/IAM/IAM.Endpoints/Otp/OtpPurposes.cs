using Common.InterModuleRequests.IAM;

namespace IAM.Endpoints.Otp;

internal static class OtpPurposes
{
    internal const string Login = "login";
    internal const string Registration = "registration";

    /// <summary>Purpose the initial 6-digit email code is sent and verified under, before a verification token is issued.</summary>
    internal const string EmailVerification = "email_verification";

    /// <summary>Purpose a post-verification token is stored under when the address already has a user: consumed by CreateByEmail. Defined once in <see cref="EmailVerificationPurposes.ExistingAddress" />, which other modules verify under.</summary>
    internal const string EmailVerifiedLogin = EmailVerificationPurposes.ExistingAddress;

    /// <summary>Purpose a post-verification token is stored under when the address has no user yet: consumed by self-registration. The same purpose other modules verify under, so it is defined once in <see cref="EmailVerificationPurposes.NewAddress" />.</summary>
    internal const string EmailVerifiedRegister = EmailVerificationPurposes.NewAddress;
}
