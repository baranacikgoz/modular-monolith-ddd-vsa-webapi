using Common.Domain.EventBus;
using Common.IntegrationEvents;
using Microsoft.Extensions.Logging;
using Notifications.Application;

namespace Notifications.Infrastructure.IntegrationEventHandlers;

public partial class UserRegisteredIntegrationEventHandler(
    ISmsService smsService,
    ILogger<UserRegisteredIntegrationEventHandler> logger
    ) : EventHandlerBase<UserRegisteredIntegrationEvent>
{
    protected override async Task HandleAsync(UserRegisteredIntegrationEvent @event, CancellationToken cancellationToken)
    {
        LogSendingWelcomeSms(logger, @event.UserId.Value);

        await smsService.SendWelcomeAsync(@event.Name, @event.PhoneNumber);
    }

    [LoggerMessage(
    Level = LogLevel.Debug,
    Message = "Sending welcome sms to the new user {UserId}.")]
    private static partial void LogSendingWelcomeSms(ILogger logger, Guid userId);
}
