using System.Security.Claims;
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
    private static readonly string[] _protectedPrefixes =
    [
        "notifications:user:",
        "notifications:role:"
    ];

    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier
                     ?? throw new HubException("Unauthenticated connection rejected.");

        await Groups.AddToGroupAsync(Context.ConnectionId, NotificationGroupName.ForUser(userId));

        foreach (var role in Context.User!.Claims
                     .Where(c => c.Type == ClaimTypes.Role)
                     .Select(c => c.Value))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, NotificationGroupName.ForRole(role));
        }

        NotificationsTelemetry.ActiveConnections.Add(1);
        LogConnected(logger, userId, Context.ConnectionId);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        NotificationsTelemetry.ActiveConnections.Add(-1);
        LogDisconnected(logger, Context.UserIdentifier ?? "unknown", Context.ConnectionId, exception);
        await base.OnDisconnectedAsync(exception);
    }

    public async Task SubscribeAsync(string groupName)
    {
        if (_protectedPrefixes.Any(p => groupName.StartsWith(p, StringComparison.Ordinal)))
        {
            throw new HubException("Cannot manually subscribe to user or role groups.");
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    public async Task UnsubscribeAsync(string groupName)
    {
        if (_protectedPrefixes.Any(p => groupName.StartsWith(p, StringComparison.Ordinal)))
        {
            throw new HubException("Cannot manually unsubscribe from user or role groups.");
        }

        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }

    [LoggerMessage(Level = LogLevel.Information,
        Message = "User {UserId} connected via SignalR (connId={ConnectionId}).")]
    private static partial void LogConnected(ILogger logger, string userId, string connectionId);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "User {UserId} disconnected from SignalR (connId={ConnectionId}).")]
    private static partial void LogDisconnected(ILogger logger, string userId, string connectionId,
        Exception? exception);
}
