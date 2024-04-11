using Common.EventBus.Contracts;
using Microsoft.Extensions.Logging;
using Common.Events;
using MassTransit;

namespace Notifications.EventConsumers;

public partial class UserCreatedEventHandler(
    ILogger<UserCreatedEventHandler> logger
    ) : EventHandlerBase<UserCreatedDomainEvent>
{
    protected override Task HandleAsync(UserCreatedDomainEvent @event, CancellationToken cancellationToken)
    {
        LogUserRegistered(logger, $"{@event.Name} {@event.LastName}");

        // Do something with the event here.

        return Task.CompletedTask;
    }

    [LoggerMessage(
    Level = LogLevel.Debug,
    Message = "A new user with the name {FullName} has been registered.")]
    private static partial void LogUserRegistered(ILogger logger, string fullName);
}
