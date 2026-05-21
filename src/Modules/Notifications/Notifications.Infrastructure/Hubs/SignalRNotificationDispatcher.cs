using Common.Domain.StronglyTypedIds;
using Microsoft.AspNetCore.SignalR;
using Notifications.Application.Hubs;
using Notifications.Infrastructure.Telemetry;

namespace Notifications.Infrastructure.Hubs;

internal sealed class SignalRNotificationDispatcher(
    IHubContext<NotificationsHub, INotificationsClient> hubContext
) : INotificationDispatcher
{
    public async Task SendToUserAsync(ApplicationUserId userId, NotificationPayload payload, CancellationToken cancellationToken)
    {
        await hubContext.Clients.Group(NotificationGroupName.ForUser(userId)).ReceiveNotification(payload);
        NotificationsTelemetry.NotificationsSent.Add(1);
    }

    public async Task SendToGroupAsync(string groupName, NotificationPayload payload, CancellationToken cancellationToken)
    {
        await hubContext.Clients.Group(groupName).ReceiveNotification(payload);
        NotificationsTelemetry.NotificationsSent.Add(1);
    }

    public async Task SendToAllAsync(NotificationPayload payload, CancellationToken cancellationToken)
    {
        await hubContext.Clients.All.ReceiveNotification(payload);
        NotificationsTelemetry.NotificationsSent.Add(1);
    }

    public async Task SendToAllExceptAsync(IReadOnlyList<string> excludedConnectionIds, NotificationPayload payload, CancellationToken cancellationToken)
    {
        await hubContext.Clients.AllExcept(excludedConnectionIds).ReceiveNotification(payload);
        NotificationsTelemetry.NotificationsSent.Add(1);
    }
}
