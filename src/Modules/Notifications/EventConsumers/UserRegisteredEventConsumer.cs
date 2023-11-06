using Common.Eventbus;
using MassTransit;
using Microsoft.Extensions.Logging;
using Common.DomainEvents;

namespace Notifications.EventConsumers;

public partial class UserRegisteredEventConsumer(
    ILogger<UserRegisteredEventConsumer> logger
    ) : IEventConsumer<Events.IdentityAndAuth.UserCreatedEvent>
{
    public Task Consume(ConsumeContext<Events.IdentityAndAuth.UserCreatedEvent> context)
    {
        var userCreatedEvent = context.Message;
        LogUserRegisteredEventConsumer(logger, userCreatedEvent.UserId);

        // Do something with the event here.

        return Task.CompletedTask;
    }

    [LoggerMessage(
        Level = LogLevel.Debug,
        Message = "UserRegisteredEventConsumer: UserId: {UserId}")]
    private static partial void LogUserRegisteredEventConsumer(ILogger logger, Guid userId);
}
