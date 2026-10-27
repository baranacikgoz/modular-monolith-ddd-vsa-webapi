using Common.Domain.StronglyTypedIds;

namespace Notifications.Infrastructure.Hubs;

internal static class NotificationGroupName
{
    private const string OfflineAccessRole = "offline_access";
    private const string UmaAuthorizationRole = "uma_authorization";
    private const string DefaultRolePrefix = "default-roles-";

    public static string ForUser(string userId) => $"notifications:user:{userId}";
    public static string ForUser(ApplicationUserId userId) => ForUser(userId.ToString());

    /// <summary>Every connected user holding a realm role (the hub joins them on connect from the token's roles claim).</summary>
    public static string ForRole(string role) => $"notifications:role:{role}";

    /// <summary>
    ///     False for the roles Keycloak gives every user of a realm (<c>offline_access</c>, <c>uma_authorization</c> and the
    ///     realm's <c>default-roles-*</c> composite): they come with the token's roles claim, but no alert is addressed to
    ///     them, so a connection keeps no group for them.
    /// </summary>
    public static bool IsAddressableRole(string role) =>
        role is not (OfflineAccessRole or UmaAuthorizationRole)
        && !role.StartsWith(DefaultRolePrefix, StringComparison.Ordinal);
}
