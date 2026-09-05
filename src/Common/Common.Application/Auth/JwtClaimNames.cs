namespace Common.Application.Auth;

/// <summary>
///     Claim names as Keycloak emits them. <c>MapInboundClaims</c> is off, so these are the exact wire names.
/// </summary>
public static class JwtClaimNames
{
    public const string Subject = "sub";
    public const string Jti = "jti";
    public const string SessionId = "sid";
    public const string Roles = "roles";
    public const string PreferredUsername = "preferred_username";
    public const string Email = "email";
    public const string AuthorizedParty = "azp";
    public const string Expiration = "exp";
}
