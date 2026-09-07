namespace IAM.Domain.Users;

public static class Constants
{
    public const int NameMaxLength = 100;
    public const int PhoneNumberLength = 12;
    public const int EmailMaxLength = 255;
    public const int PasswordMaxLength = 256;
    public const int SearchTermMaxLength = 256;

    // Keycloak refresh tokens are compact JWTs; a few hundred bytes today, bounded for request validation.
    public const int RefreshTokenMaxLength = 4096;
}
