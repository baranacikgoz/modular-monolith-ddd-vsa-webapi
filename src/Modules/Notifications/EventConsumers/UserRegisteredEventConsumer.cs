using Common.EventBus.Contracts;
using Microsoft.Extensions.Logging;
using Common.Events;

namespace Notifications.EventConsumers;

internal partial class UserRegisteredEventConsumer(
    ILogger<UserRegisteredEventConsumer> logger
    ) : IEventHandler<EventsOf.IdentityAndAuth.UserCreatedDomainEvent>
{
    public Task HandleAsync(EventsOf.IdentityAndAuth.UserCreatedDomainEvent notification, CancellationToken cancellationToken)
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
