using Common.Application.BackgroundJobs;
using Common.Application.EventBus;
using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
using Microsoft.Extensions.Logging;

namespace Notifications.Application.IntegrationEventHandlers;

public partial class UserRegisteredIntegrationEventHandler(
    IBackgroundJobs backgroundJobs,
    ILogger<UserRegisteredIntegrationEventHandler> logger
    ) : EventHandlerBase<UserRegisteredIntegrationEvent>
{
    protected override Task HandleAsync(UserRegisteredIntegrationEvent @event, CancellationToken cancellationToken)
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
