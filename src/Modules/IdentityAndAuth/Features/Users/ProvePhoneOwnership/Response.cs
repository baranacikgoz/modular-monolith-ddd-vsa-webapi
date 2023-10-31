namespace IdentityAndAuth.Features.Users.ProvePhoneOwnership;

public sealed record Response(bool UserExists, string PhoneVerificationToken);
