namespace IAM.Endpoints.Otp.VersionNeutral.VerifyEmail;

public sealed record Response
{
    public required bool IsRegistered { get; init; }
    public required string EmailVerificationToken { get; init; }
}
