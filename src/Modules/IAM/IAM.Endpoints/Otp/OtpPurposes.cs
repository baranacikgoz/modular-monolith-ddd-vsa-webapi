namespace IAM.Endpoints.Otp;

internal static class OtpPurposes
{
    internal const string Login = "login";
    internal const string Registration = "registration";

    /// <summary>Purpose the initial 6-digit email code is sent and verified under, before a verification token is issued.</summary>
    internal const string EmailVerification = "email_verification";

    /// <summary>Purpose a post-verification token is stored under when the address already has a user: consumed by CreateByEmail.</summary>
    internal const string EmailVerifiedLogin = "email_verified_login";

    /// <summary>Purpose a post-verification token is stored under when the address has no user yet: consumed by self-registration.</summary>
    internal const string EmailVerifiedRegister = "email_verified_register";
}
