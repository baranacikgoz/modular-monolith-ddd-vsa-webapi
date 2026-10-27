namespace Common.Application.Auth;

/// <summary>Realm role names. Assigned and evaluated in Keycloak; the API only reads them off the token.</summary>
public static class KeycloakRoles
{
    public const string Basic = "basic";
    public const string Staff = "staff";
    public const string SystemAdmin = "system-admin";

    /// <summary>Business role of a user whose store was approved. Granted by IAM when the approval event arrives, never at registration.</summary>
    public const string StoreOwner = "store-owner";
}
