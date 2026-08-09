using Common.Application.Caching;
using Common.Application.EventBus;
using IAM.Domain.Identity.DomainEvents.v1;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Application.Users.DomainEventHandlers.v1;

public class V1SessionRefreshedCacheInvalidationHandler(IFusionCache cache)
    : DomainEventHandlerBase<V1SessionRefreshedDomainEvent>
{
    public override async Task HandleAsync(V1SessionRefreshedDomainEvent @event, CancellationToken cancellationToken)
    {
        // Also raised on a plain token rotation (session already valid), where this is a harmless
        // no-op. It is required on Session.Reactivate(), which clears RevokedAt on a re-login to a
        // previously revoked device: without this, the stale cached "revoked" entry would keep
        // locking that session out until its TTL expires.
        await cache.RemoveAsync(CacheKeys.For.SessionValid(@event.SessionId.Value), token: cancellationToken);
    }
}
