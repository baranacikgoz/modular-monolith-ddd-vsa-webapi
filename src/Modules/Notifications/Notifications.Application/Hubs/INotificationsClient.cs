namespace Notifications.Application.Hubs;

public interface INotificationsClient
{
    Task ReceiveNotification(NotificationPayload payload);
}
