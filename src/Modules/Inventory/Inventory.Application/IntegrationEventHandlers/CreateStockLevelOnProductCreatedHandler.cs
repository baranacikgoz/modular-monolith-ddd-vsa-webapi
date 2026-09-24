using Common.Application.EventBus;
using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.IntegrationEvents;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Inventory.Application.IntegrationEventHandlers;

/// <summary>
///     The base class's cache pre-filter plus transactional inbox (see IntegrationEventHandlerBase) already
///     cover MassTransit redelivery of the same message. This handler adds a domain-level idempotency check on
///     top for a different case: two distinct messages (different Ids) announcing the same product, which no
///     per-message inbox can tell apart. Never assume a single dedupe layer is enough for a mutation that
///     would otherwise create a duplicate row.
/// </summary>
public sealed partial class CreateStockLevelOnProductCreatedHandler(
    IInventoryDbContext dbContext,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    IInboxStore<IInventoryDbContext> inbox,
    ILogger<CreateStockLevelOnProductCreatedHandler> logger
) : IntegrationEventHandlerBase<ProductCreatedIntegrationEvent>(cache, cachingOptions, inbox, logger)
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
