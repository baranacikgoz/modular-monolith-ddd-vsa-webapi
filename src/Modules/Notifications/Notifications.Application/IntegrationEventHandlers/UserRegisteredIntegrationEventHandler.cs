using Common.Application.EventBus;
using Common.Application.Options;
using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Application.IntegrationEventHandlers;

public partial class UserRegisteredIntegrationEventHandler(
    ILogger<UserRegisteredIntegrationEventHandler> logger,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    ISmsService smsService
) : EventHandlerBase<UserRegisteredIntegrationEvent>(cache, cachingOptions, logger)
{
    protected override async Task ProcessAsync(UserRegisteredIntegrationEvent @event,
        CancellationToken cancellationToken)
    {
        LogSendingWelcomeSms(logger, @event.UserId);
        await smsService.SendWelcomeAsync(@event.Name, @event.PhoneNumber);
    }

    [LoggerMessage(
        Level = LogLevel.Debug,
        Message = "Sending welcome sms job to the new user {UserId}.")]
    private static partial void LogSendingWelcomeSms(ILogger logger, ApplicationUserId userId);
}
