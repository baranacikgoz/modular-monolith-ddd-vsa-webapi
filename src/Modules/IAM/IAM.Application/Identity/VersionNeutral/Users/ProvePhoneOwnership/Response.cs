namespace IAM.Application.Identity.VersionNeutral.Users.ProvePhoneOwnership;

public sealed record Response(bool UserExists, string PhoneVerificationToken);
