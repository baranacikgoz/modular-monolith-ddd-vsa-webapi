using Common.Application.BackgroundJobs;
using Common.Application.EventBus;
using Common.Application.Options;
using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Application.IntegrationEventHandlers;

public partial class UserRegisteredIntegrationEventHandler(
    IBackgroundJobs backgroundJobs,
    ILogger<UserRegisteredIntegrationEventHandler> logger,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions
) : EventHandlerBase<UserRegisteredIntegrationEvent>(cache, cachingOptions)
{
    protected override Task HandleAsync(ConsumeContext<UserRegisteredIntegrationEvent> context,
        UserRegisteredIntegrationEvent @event, CancellationToken cancellationToken)
    {
        LogEnqueuingSendingWelcomeSms(logger, @event.UserId);

        backgroundJobs.Enqueue<ISmsService>(smsService => smsService.SendWelcomeAsync(@event.Name, @event.PhoneNumber));

        return Task.CompletedTask;
    }

    [LoggerMessage(
        Level = LogLevel.Debug,
        Message = "Enqueuing sending welcome sms job to the new user {UserId}.")]
    private static partial void LogEnqueuingSendingWelcomeSms(ILogger logger, ApplicationUserId userId);
}
