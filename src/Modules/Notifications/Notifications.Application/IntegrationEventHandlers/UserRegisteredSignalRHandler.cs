using Common.Application.EventBus;
using Common.Application.Options;
using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Notifications.Application.Hubs;
using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Application.IntegrationEventHandlers;

public partial class UserRegisteredSignalRHandler(
    ILogger<UserRegisteredSignalRHandler> logger,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    INotificationDispatcher notificationDispatcher
) : IntegrationEventHandlerBase<UserRegisteredIntegrationEvent>(cache, cachingOptions, logger)
{
    protected override async Task ProcessAsync(UserRegisteredIntegrationEvent @event,
        CancellationToken cancellationToken)
    {
        LogDispatchingNotification(logger, @event.UserId);
        await notificationDispatcher.SendToUserAsync(
            @event.UserId,
            new NotificationPayload("user.registered", @event.UserId.ToString()),
            cancellationToken);
    }

    [LoggerMessage(
        Level = LogLevel.Debug,
        Message = "Dispatching real-time notification to user {UserId}.")]
    private static partial void LogDispatchingNotification(ILogger logger, ApplicationUserId userId);
}
