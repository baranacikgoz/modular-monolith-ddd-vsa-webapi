namespace IAM.Application.Auth;

public static class CustomRoles
{
    // Level 0
    public const string SystemAdmin = nameof(SystemAdmin); // Refers to the system admins.

    // Level 1
    public const string Basic = nameof(Basic); // Refers to the individual users.

    public static readonly HashSet<string> All = [SystemAdmin, Basic];
}
