using Common.Domain.StronglyTypedIds;
using Microsoft.AspNetCore.SignalR;
using Notifications.Application.Hubs;
using Notifications.Infrastructure.Telemetry;

namespace Notifications.Infrastructure.Hubs;

internal sealed class SignalRNotificationDispatcher(
    IHubContext<NotificationsHub, INotificationsClient> hubContext
) : INotificationDispatcher
{
    public Task SendToUserAsync(ApplicationUserId userId, NotificationPayload payload, CancellationToken cancellationToken) =>
        DispatchAsync(hubContext.Clients.Group(NotificationGroupName.ForUser(userId)), payload);

    public Task SendToGroupAsync(string groupName, NotificationPayload payload, CancellationToken cancellationToken) =>
        DispatchAsync(hubContext.Clients.Group(groupName), payload);

    public Task SendToAllAsync(NotificationPayload payload, CancellationToken cancellationToken) =>
        DispatchAsync(hubContext.Clients.All, payload);

    public Task SendToAllExceptAsync(IReadOnlyList<string> excludedConnectionIds, NotificationPayload payload, CancellationToken cancellationToken) =>
        DispatchAsync(hubContext.Clients.AllExcept(excludedConnectionIds), payload);

    private static async Task DispatchAsync(INotificationsClient client, NotificationPayload payload)
    {
        try
        {
            await client.ReceiveNotification(payload);
        }
        catch (Exception ex)
        {
            NotificationsTelemetry.RecordSignalRError("send", ex.GetType().Name);
            throw;
        }

        NotificationsTelemetry.RecordNotificationSent(payload.Type);
    }
}
