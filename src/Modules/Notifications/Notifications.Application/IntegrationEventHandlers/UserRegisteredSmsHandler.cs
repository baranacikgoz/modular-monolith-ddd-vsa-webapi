using Common.Application.EventBus;
using Common.Application.Options;
using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Application.IntegrationEventHandlers;

public partial class UserRegisteredSmsHandler(
    ILogger<UserRegisteredSmsHandler> logger,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    ISmsService smsService
) : IntegrationEventHandlerBase<UserRegisteredIntegrationEvent>(cache, cachingOptions, logger)
{
    protected override async Task ProcessAsync(UserRegisteredIntegrationEvent @event,
        CancellationToken cancellationToken)
    {
        LogSendingWelcomeSms(logger, @event.UserId);
        await smsService.SendWelcomeAsync(@event.FullName, @event.PhoneNumber);
    }

    [LoggerMessage(
        Level = LogLevel.Debug,
        Message = "Sending welcome SMS to new user {UserId}.")]
    private static partial void LogSendingWelcomeSms(ILogger logger, ApplicationUserId userId);
}
