using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Notifications.Application.Hubs;
using Notifications.Infrastructure.Telemetry;

namespace Notifications.Infrastructure.Hubs;

[Authorize]
internal sealed partial class NotificationsHub(ILogger<NotificationsHub> logger)
    : Hub<INotificationsClient>
{
    public override async Task OnConnectedAsync()
    {
        try
        {
            var userId = Context.UserIdentifier
                         ?? throw new HubException("Unauthenticated connection rejected.");

            await Groups.AddToGroupAsync(Context.ConnectionId, NotificationGroupName.ForUser(userId));

            NotificationsTelemetry.ActiveConnections.Add(1);
            LogConnected(logger, userId, Context.ConnectionId);

            await base.OnConnectedAsync();
        }
        catch (Exception ex)
        {
            NotificationsTelemetry.RecordSignalRError("connect", ex.GetType().Name);
            throw;
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        NotificationsTelemetry.ActiveConnections.Add(-1);
        LogDisconnected(logger, Context.UserIdentifier ?? "unknown", Context.ConnectionId, exception);
        await base.OnDisconnectedAsync(exception);
    }

    [LoggerMessage(Level = LogLevel.Information,
        Message = "User {UserId} connected via SignalR (connId={ConnectionId}).")]
    private static partial void LogConnected(ILogger logger, string userId, string connectionId);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "User {UserId} disconnected from SignalR (connId={ConnectionId}).")]
    private static partial void LogDisconnected(ILogger logger, string userId, string connectionId,
        Exception? exception);
}
