namespace Common.Application.Auth;

/// <summary>Realm role names. Assigned and evaluated in Keycloak; the API only reads them off the token.</summary>
public static class KeycloakRoles
{
    public const string Basic = "basic";
    public const string Staff = "staff";
    public const string SystemAdmin = "system-admin";
}
