namespace IAM.Domain.Identity.Sessions;

public static class Constants
{
    public const int ClientIdMaxLength = 50;
    public const int DeviceNameMaxLength = 100;
    public const int PushTokenMaxLength = 4096;

    // Convert.ToBase64String of the 32-byte token TokenService generates is always exactly 44 chars.
    public const int RefreshTokenBase64MaxLength = 44;
}
