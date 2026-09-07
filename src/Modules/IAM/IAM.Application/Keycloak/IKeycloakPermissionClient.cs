namespace IAM.Application.Keycloak;

/// <summary>
///     Keycloak Authorization Services policy decision point, queried with the caller's own access token
///     (<c>grant_type=urn:ietf:params:oauth:grant-type:uma-ticket</c>).
/// </summary>
public interface IKeycloakPermissionClient
{
    /// <summary>
    ///     True when the token holder is granted <paramref name="permission" /> (<c>resource#scope</c>). False on a
    ///     denial or when Keycloak no longer accepts the token (revoked session). Throws when Keycloak cannot be reached,
    ///     so the caller fails closed instead of guessing.
    /// </summary>
    Task<bool> DecideAsync(string accessToken, string permission, CancellationToken cancellationToken);

    /// <summary>Every resource/scope pair the token holder is granted.</summary>
    Task<IReadOnlyList<GrantedPermission>> ListPermissionsAsync(string accessToken, CancellationToken cancellationToken);
}
