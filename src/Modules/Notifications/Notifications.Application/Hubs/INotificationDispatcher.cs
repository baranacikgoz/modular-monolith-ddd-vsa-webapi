using Common.Domain.StronglyTypedIds;

namespace Notifications.Application.Hubs;

public interface INotificationDispatcher
{
    Task SendToUserAsync(ApplicationUserId userId, NotificationPayload payload, CancellationToken cancellationToken);
    Task SendToGroupAsync(string groupName, NotificationPayload payload, CancellationToken cancellationToken);
    Task SendToAllAsync(NotificationPayload payload, CancellationToken cancellationToken);
    Task SendToAllExceptAsync(IReadOnlyList<string> excludedConnectionIds, NotificationPayload payload, CancellationToken cancellationToken);
}
