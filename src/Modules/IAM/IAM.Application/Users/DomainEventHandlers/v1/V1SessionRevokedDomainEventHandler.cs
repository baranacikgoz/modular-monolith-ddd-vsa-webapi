using Common.Application.EventBus;
using Common.IntegrationEvents;
using IAM.Domain.Identity.DomainEvents.v1;
using IAM.Domain.Identity.Sessions;

namespace IAM.Application.Users.DomainEventHandlers.v1;

public class V1SessionRevokedDomainEventHandler(IIntegrationEventOutbox outbox)
    : DomainEventHandlerBase<V1SessionRevokedDomainEvent>
{
    public override Task HandleAsync(V1SessionRevokedDomainEvent @event, CancellationToken cancellationToken)
    {
        // Only a theft signal warrants alerting the user — a normal sign-out needs no notification.
        if (@event.Reason == SessionRevokedReason.TokenReuseDetected)
        {
            outbox.Collect(new SessionTokenReuseDetectedIntegrationEvent(
                @event.UserId, @event.SessionId.Value, DeviceName: null));
        }

        return Task.CompletedTask;
    }
}
