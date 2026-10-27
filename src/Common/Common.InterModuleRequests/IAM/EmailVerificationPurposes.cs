namespace Common.InterModuleRequests.IAM;

/// <summary>
///     Purposes under which IAM's <c>POST /otp/email/verify</c> stores the single-use verification token it hands out once a
///     caller has proven they own a mailbox. A module that needs that proof before it creates an account for an address
///     (the anonymous store application) checks the token the client sends with a
///     <c>VerifyEmailOtpRequest</c> carrying the normalized address, the token and one of these purposes; checking consumes the token.
/// </summary>
public static class EmailVerificationPurposes
{
    /// <summary>The token issued for an address that has no user yet.</summary>
    public const string NewAddress = "email_verified_register";
}
