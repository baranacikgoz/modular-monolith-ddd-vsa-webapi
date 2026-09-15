namespace IAM.Endpoints.Common;

/// <summary>
///     Keycloak stores emails lowercased; every caller of an email-identity endpoint normalizes at this
///     one boundary so lookup, send and quota keys never split by casing.
/// </summary>
internal static class EmailNormalization
{
    // CA1308 wants ToUpperInvariant for security comparisons; here lowercase is the correct target
    // because it must match Keycloak's own storage casing, not just compare safely.
#pragma warning disable CA1308
    internal static string Normalize(string email) => email.Trim().ToLowerInvariant();
#pragma warning restore CA1308
}
