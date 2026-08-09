using Common.Application.Caching;
using Common.Application.EventBus;
using IAM.Application.Persistence;
using IAM.Domain.Identity.DomainEvents.v1;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Application.Users.DomainEventHandlers.v1;

public class V1AllSessionsRevokedCacheInvalidationHandler(IIAMDbContext dbContext, IFusionCache cache)
    : DomainEventHandlerBase<V1AllSessionsRevokedDomainEvent>
{
    public override async Task HandleAsync(V1AllSessionsRevokedDomainEvent @event, CancellationToken cancellationToken)
    {
        // The event carries only UserId, not the affected session ids (adding them would be a
        // frozen V1 event edit, see CLAUDE.md versioning rule), so look them up. This dispatches
        // pre-commit but session ids are immutable, so the pre-commit set matches what will land.
        var sessionIds = await dbContext.Sessions
            .AsNoTracking()
            .Where(s => s.UserId == @event.UserId)
            .Select(s => s.Id)
            .ToListAsync(cancellationToken);

        foreach (var sessionId in sessionIds)
        {
            await cache.RemoveAsync(CacheKeys.For.SessionValid(sessionId.Value), token: cancellationToken);
        }
    }
}
