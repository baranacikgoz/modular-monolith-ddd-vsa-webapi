namespace IAM.Application.Identity.VersionNeutral.Users.VerifyOtp;

public sealed record Response
{
    public required bool IsRegistered { get; init; }
    public required string PhoneVerificationToken { get; init; }
}
