using Common.Application.EventBus;
using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.Application.Persistence.Projections;
using Common.IntegrationEvents;
using Inventory.Application.Persistence;
using Inventory.Application.StoreAccesses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Inventory.Application.IntegrationEventHandlers;

/// <summary>
///     Keeps <see cref="StoreAccess" /> in step with Products. A stale or duplicate event (its version is not newer than the
///     row's) is dropped by <c>UpsertIfNewerAsync</c>, so delivery order does not matter.
/// </summary>
public sealed class UpdateStoreAccessOnStoreAccessChangedHandler(
    IInventoryDbContext dbContext,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    IInboxStore<IInventoryDbContext> inbox,
    ILogger<UpdateStoreAccessOnStoreAccessChangedHandler> logger
) : IntegrationEventHandlerBase<StoreAccessChangedIntegrationEvent>(cache, cachingOptions, inbox, logger)
{
    protected override async Task ProcessAsync(StoreAccessChangedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        await dbContext.StoreAccess.UpsertIfNewerAsync(
            @event.StoreId,
            @event.Version,
            () => StoreAccess.Create(@event.StoreId),
            row => row.Apply(@event.OwnerId, @event.IsActive),
            cancellationToken);
    }
}
