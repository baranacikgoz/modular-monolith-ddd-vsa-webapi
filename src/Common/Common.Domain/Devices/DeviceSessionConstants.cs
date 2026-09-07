namespace Common.Domain.Devices;

/// <summary>
///     Limits shared by the login contract (IAM validates the request) and the device registry (Notifications
///     persists it). Kept in the shared kernel so both modules agree without referencing each other.
/// </summary>
public static class DeviceSessionConstants
{
    public const int ClientIdMaxLength = 50;
    public const int DeviceNameMaxLength = 100;
    public const int PushTokenMaxLength = 4096;

    // Keycloak session ids are opaque url-safe strings (currently 24 chars); leave headroom.
    public const int SessionIdMaxLength = 64;
}
