namespace IAM.Infrastructure.Keycloak;

/// <summary>Relative paths under the Keycloak base URL. Every client's <c>BaseAddress</c> ends with a slash, so these must not start with one.</summary>
internal static class KeycloakPaths
{
    public static string Token(string realm) => $"realms/{realm}/protocol/openid-connect/token";

    public static string Admin(string realm, string relative) => $"admin/realms/{realm}/{relative}";
}
