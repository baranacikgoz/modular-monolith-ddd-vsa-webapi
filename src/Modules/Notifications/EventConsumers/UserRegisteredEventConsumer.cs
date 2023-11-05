using Common.DomainEvents.viaIdentityAndAuth;
using Common.Eventbus;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Notifications.EventConsumers;

public class UserRegisteredEventConsumer(
    ILogger<UserRegisteredEventConsumer> logger
    ) : IEventConsumer<Events.IdentityAndAuth.UserCreatedEvent>
{
    public Task Consume(ConsumeContext<Events.IdentityAndAuth.UserCreatedEvent> context)
    {
        var userCreatedEvent = context.Message;
        logger.LogDebug("UserRegisteredEventConsumer: UserId: {UserId}", userCreatedEvent.UserId);

        // Do something with the event here.

        return Task.CompletedTask;
    }
}
