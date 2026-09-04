using Common.Application.Caching;
using Common.Application.EventBus;
using Common.IntegrationEvents;
using IAM.Domain.Identity.DomainEvents.v1;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Application.Users.DomainEventHandlers.v1;

public class V1SessionRevokedDomainEventHandler(IIntegrationEventOutbox outbox, IFusionCache cache)
    : DomainEventHandlerBase<V1SessionRevokedDomainEvent>
{
    public override async Task HandleAsync(V1SessionRevokedDomainEvent @event, CancellationToken cancellationToken)
    {
        // Only a theft signal warrants alerting the user, a normal sign-out needs no notification.
        if (@event.Reason == V1SessionRevokedDomainEvent.ReasonSnapshot.TokenReuseDetected)
        {
            outbox.Collect(new SessionTokenReuseDetectedIntegrationEvent(
                @event.UserId, @event.SessionId.Value, DeviceName: null));
        }

        // Remove, not set-revoked: this dispatches before the transaction commits (see
        // OutboxSaveHelper), so a set here could lock out a session whose commit later rolls back.
        // A remove just forces the next auth check back to Postgres.
        await cache.RemoveAsync(CacheKeys.For.SessionValid(@event.SessionId.Value), token: cancellationToken);
    }
}
