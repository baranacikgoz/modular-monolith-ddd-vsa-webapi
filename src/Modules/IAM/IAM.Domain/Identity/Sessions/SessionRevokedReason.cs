namespace IAM.Domain.Identity.Sessions;

public enum SessionRevokedReason
{
    UserSignedOut,
    SignedOutEverywhere,
    TokenReuseDetected,
    Expired
}
