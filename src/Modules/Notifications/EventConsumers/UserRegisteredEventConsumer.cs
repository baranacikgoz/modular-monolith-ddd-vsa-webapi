using Common.Eventbus;
using Microsoft.Extensions.Logging;
using Common.DomainEvents;

namespace Notifications.EventConsumers;

internal partial class UserRegisteredEventConsumer(
    ILogger<UserRegisteredEventConsumer> logger
    ) : IEventHandler<Events.Published.From.IdentityAndAuth.UserCreated>
{
    public Task HandleAsync(Events.Published.From.IdentityAndAuth.UserCreated notification, CancellationToken cancellationToken)
    {
        LogUserRegistered(logger, notification.Name, notification.UserId);

        // Do something with the event here.

        return Task.CompletedTask;
    }

    [LoggerMessage(
        Level = LogLevel.Debug,
        Message = "A new user with the name {Name} has been registered with the id {UserId}.")]
    private static partial void LogUserRegistered(ILogger logger, string name, Guid userId);
}
