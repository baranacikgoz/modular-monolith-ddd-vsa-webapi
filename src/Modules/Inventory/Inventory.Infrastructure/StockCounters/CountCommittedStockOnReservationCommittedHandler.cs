using Common.Application.EventBus;
using Common.Application.Extensions;
using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.Domain.ResultMonad;
using Common.IntegrationEvents;
using Common.Infrastructure.Persistence.Extensions;
using Inventory.Application.Persistence;
using Inventory.Application.ProductSummaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Inventory.Infrastructure.StockCounters;

/// <summary>
///     Counts a committed reservation into its store's totals for the business day the event was raised on. The store is
///     read from the local product copy; a product that is not there yet fails the message so the bus retries once the
///     copy has caught up (the nightly reconciliation fills it for products that predate it). Each consumer is one atomic
///     upsert plus its inbox row in one transaction (<see cref="StockCounterWriter" />), so a redelivery never counts twice.
/// </summary>
public sealed class CountCommittedStockOnReservationCommittedHandler(
    IInventoryDbContext dbContext,
    TimeProvider timeProvider,
    IOptions<InventoryOptions> inventoryOptions,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    IInboxStore<IInventoryDbContext> inbox,
    ILogger<CountCommittedStockOnReservationCommittedHandler> logger
) : IntegrationEventHandlerBase<StockReservationCommittedIntegrationEvent>(cache, cachingOptions, inbox, logger)
{
    protected override Task ProcessAsync(StockReservationCommittedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        var businessTimeZone = TimeZoneInfo.FindSystemTimeZoneById(inventoryOptions.Value.BusinessTimeZoneId);

        return StockCounterWriter.InTransactionAsync(
            dbContext,
            async () =>
            {
                var storeId = await StoreOfAsync(@event.ProductId, cancellationToken);

                await StockCounterWriter.AddCommittedAsync(
                    dbContext, storeId, @event.Quantity, @event.CreatedOn.ToDate(businessTimeZone), timeProvider.GetUtcNow(),
                    cancellationToken);
            },
            cancellationToken);
    }

    private async Task<DefaultIdType> StoreOfAsync(DefaultIdType productId, CancellationToken cancellationToken)
    {
        var store = await dbContext.ProductSummaries
            .AsNoTracking()
            .TagWith(nameof(StoreOfAsync), productId)
            .Where(p => p.ProductId == productId)
            .Select(p => new { p.StoreId })
            .SingleAsResultAsync(nameof(ProductSummary), cancellationToken);

        return store.Match(
            onSuccess: found => found.StoreId,
            onFailure: _ => throw new InvalidOperationException(
                $"No store is known for product {productId} yet; the message is retried once the product copy has it."));
    }
}
