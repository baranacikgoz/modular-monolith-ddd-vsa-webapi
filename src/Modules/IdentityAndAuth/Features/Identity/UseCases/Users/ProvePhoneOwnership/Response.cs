namespace IdentityAndAuth.Features.Identity.UseCases.Users.ProvePhoneOwnership;

public sealed record Response(bool UserExists, string PhoneVerificationToken);
