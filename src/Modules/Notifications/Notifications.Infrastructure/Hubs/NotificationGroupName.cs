using Common.Domain.StronglyTypedIds;

namespace Notifications.Infrastructure.Hubs;

internal static class NotificationGroupName
{
    public static string ForUser(string userId) => $"notifications:user:{userId}";
    public static string ForUser(ApplicationUserId userId) => ForUser(userId.ToString());
}
