namespace Notifications.Application.Hubs;

public sealed record NotificationPayload(string Type, string? ResourceId = null);
