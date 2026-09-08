using Common.Application.EventBus;
using Common.Application.Options;
using Common.IntegrationEvents;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Inventory.Application.IntegrationEventHandlers;

/// <summary>
///     The base class's cache-based dedupe (see IntegrationEventHandlerBase) already covers normal MassTransit
///     redelivery. This handler adds a second, domain-level idempotency check on top - a belt-and-suspenders
///     guard against the rarer case of a cache eviction or bug bypassing the first layer, mirroring IAM's
///     BusinessOwnerPromotionHandler pattern: never assume a single dedupe layer is enough for a mutation that
///     would otherwise create a duplicate row.
/// </summary>
public sealed partial class CreateStockLevelOnProductCreatedHandler(
    IInventoryDbContext dbContext,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    ILogger<CreateStockLevelOnProductCreatedHandler> logger
) : IntegrationEventHandlerBase<ProductCreatedIntegrationEvent>(cache, cachingOptions, logger)
{
    protected override async Task ProcessAsync(ProductCreatedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        var alreadyExists = await dbContext.StockLevels
            .AsNoTracking()
            .AnyAsync(s => s.ProductId == @event.ProductId, cancellationToken);

        if (alreadyExists)
        {
            LogStockLevelAlreadyExists(logger, @event.ProductId);
            return;
        }

        var stockLevel = StockLevel.Create(@event.ProductId, @event.Quantity);
        dbContext.StockLevels.Add(stockLevel);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    [LoggerMessage(Level = LogLevel.Information,
        Message = "StockLevel for product {ProductId} already exists, skipping duplicate creation.")]
    private static partial void LogStockLevelAlreadyExists(ILogger logger, DefaultIdType productId);
}
