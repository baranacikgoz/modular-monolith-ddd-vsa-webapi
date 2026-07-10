using Common.Application.EventBus;
using Common.Application.Options;
using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Notifications.Application.Hubs;
using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Application.IntegrationEventHandlers;

public partial class SessionTokenReuseDetectedSignalRHandler(
    ILogger<SessionTokenReuseDetectedSignalRHandler> logger,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    INotificationDispatcher notificationDispatcher
) : IntegrationEventHandlerBase<SessionTokenReuseDetectedIntegrationEvent>(cache, cachingOptions, logger)
{
    protected override async Task ProcessAsync(SessionTokenReuseDetectedIntegrationEvent @event,
        CancellationToken cancellationToken)
    {
        LogDispatchingNotification(logger, @event.UserId, @event.SessionId);
        await notificationDispatcher.SendToUserAsync(
            @event.UserId,
            new NotificationPayload("session.token_reuse_detected", @event.SessionId.ToString()),
            cancellationToken);
    }

    [LoggerMessage(
        Level = LogLevel.Warning,
        Message = "Dispatching token-reuse security alert to user {UserId} for session {SessionId}.")]
    private static partial void LogDispatchingNotification(ILogger logger, ApplicationUserId userId, Guid sessionId);
}
