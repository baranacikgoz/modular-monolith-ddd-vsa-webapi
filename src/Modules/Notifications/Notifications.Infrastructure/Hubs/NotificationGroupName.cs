using Common.Domain.StronglyTypedIds;

namespace Notifications.Infrastructure.Hubs;

internal static class NotificationGroupName
{
    public static string ForUser(string userId) => $"notifications:user:{userId}";
    public static string ForUser(ApplicationUserId userId) => ForUser(userId.ToString());

    /// <summary>Every connected user holding a realm role (the hub joins them on connect from the token's roles claim).</summary>
    public static string ForRole(string role) => $"notifications:role:{role}";
}
