namespace Common.Application.Auth;

/// <summary>
/// Short, OIDC-friendly JWT claim names emitted on the wire (instead of the verbose Microsoft
/// <c>ClaimTypes</c> URIs) so the frontend can decode the token cleanly.
/// </summary>
public static class JwtClaimNames
{
    /// <summary>
    /// Roles claim. Always serialized as a JSON array (e.g. <c>["Basic"]</c>) — even for a single
    /// role — so the frontend never has to branch on string-vs-array.
    /// </summary>
    public const string Roles = "roles";

    /// <summary>
    /// Session id claim ("sid") — identifies which device/app session this access token belongs to.
    /// </summary>
    public const string SessionId = "sid";

    /// <summary>
    /// Direct permission grant claim, emitted once per permission (e.g. by API-key principals that
    /// have no role to derive permissions from). Value format matches <c>CustomPermission.NameFor</c>.
    /// </summary>
    public const string Permission = "permission";
}
