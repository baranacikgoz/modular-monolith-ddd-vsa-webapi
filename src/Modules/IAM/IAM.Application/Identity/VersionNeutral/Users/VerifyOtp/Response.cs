namespace IAM.Application.Identity.VersionNeutral.Users.VerifyOtp;

public sealed record Response(bool IsRegistered, string PhoneVerificationToken);
