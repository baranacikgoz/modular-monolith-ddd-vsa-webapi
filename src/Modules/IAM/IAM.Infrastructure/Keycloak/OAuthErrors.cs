namespace IAM.Infrastructure.Keycloak;

/// <summary>OAuth2 <c>error</c> values Keycloak's token endpoint returns, shared by the token and permission clients.</summary>
internal static class OAuthErrors
{
    public const string AccessDenied = "access_denied";
    public const string InvalidGrant = "invalid_grant";
}
