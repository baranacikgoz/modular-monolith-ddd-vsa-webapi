namespace IdentityAndAuth.Features.Auth.Domain;

internal static class CustomRoles
{
    // Level 0
    public const string SystemAdmin = nameof(SystemAdmin); // Refers to the system admins, RandevuFast's own employees.

    // Level 1
    public const string VenueAdmin = nameof(VenueAdmin); // Refers to the venue admins.

    // Level 2
    public const string Basic = nameof(Basic); // Refers to the individual users who are seeking appointments.
    public static readonly IEnumerable<string> All = new[] { SystemAdmin, VenueAdmin, Basic };
}
