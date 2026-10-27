using Common.Application.EventBus;
using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.Application.Persistence.Projections;
using Common.IntegrationEvents;
using Inventory.Application.Persistence;
using Inventory.Application.ProductSummaries;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Inventory.Application.IntegrationEventHandlers;

/// <summary>
///     Keeps <see cref="ProductSummary" /> in step with Products. A stale or duplicate event (its version is not newer
///     than the row's) is dropped by <c>UpsertIfNewerAsync</c>, so delivery order does not matter.
/// </summary>
public sealed class UpdateProductSummaryOnProductChangedHandler(
    IInventoryDbContext dbContext,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    IInboxStore<IInventoryDbContext> inbox,
    ILogger<UpdateProductSummaryOnProductChangedHandler> logger
) : IntegrationEventHandlerBase<ProductChangedIntegrationEvent>(cache, cachingOptions, inbox, logger)
{
    protected override async Task ProcessAsync(ProductChangedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        await dbContext.ProductSummaries.UpsertIfNewerAsync(
            @event.ProductId,
            @event.Version,
            () => ProductSummary.Create(@event.ProductId),
            row => row.Apply(@event.StoreId, @event.Name),
            cancellationToken);
    }
}
