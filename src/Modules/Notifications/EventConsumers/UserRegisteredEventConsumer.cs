using Common.Eventbus;
using Microsoft.Extensions.Logging;
using Common.DomainEvents;

namespace Notifications.EventConsumers;

public partial class UserRegisteredEventConsumer(
    ILogger<UserRegisteredEventConsumer> logger
    ) : IEventHandler<Events.IdentityAndAuth.UserCreatedEvent>
{
    public Task HandleAsync(Events.IdentityAndAuth.UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        LogUserRegisteredEventConsumer(logger, notification.UserId);

        // Do something with the event here.

        return Task.CompletedTask;
    }

    [LoggerMessage(
        Level = LogLevel.Debug,
        Message = "UserRegisteredEventConsumer: UserId: {UserId}")]
    private static partial void LogUserRegisteredEventConsumer(ILogger logger, Guid userId);
}
