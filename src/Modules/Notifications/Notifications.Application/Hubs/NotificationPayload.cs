namespace Notifications.Application.Hubs;

/// <summary><paramref name="Title" /> and <paramref name="Message" /> are set when the notification carries text the client shows as is; otherwise the client resolves <paramref name="ResourceId" /> itself.</summary>
public sealed record NotificationPayload(string Type, string? ResourceId = null, string? Title = null, string? Message = null);
