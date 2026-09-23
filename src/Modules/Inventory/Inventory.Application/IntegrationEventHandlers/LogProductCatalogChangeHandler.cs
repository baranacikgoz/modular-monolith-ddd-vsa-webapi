using Common.Application.EventBus;
using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.IntegrationEvents;
using Inventory.Application.Persistence;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Inventory.Application.IntegrationEventHandlers;

/// <summary>
///     A second, independent consumer of the same <see cref="ProductCreatedIntegrationEvent"/> -
///     IntegrationEventHandlerBase tracks idempotency per handler type, so multiple unrelated handlers can
///     each react to one event without interfering with each other's dedupe state.
/// </summary>
public sealed partial class LogProductCatalogChangeHandler(
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    IInboxStore<IInventoryDbContext> inbox,
    ILogger<LogProductCatalogChangeHandler> logger
) : IntegrationEventHandlerBase<ProductCreatedIntegrationEvent>(cache, cachingOptions, inbox, logger)
{
    protected override Task ProcessAsync(ProductCreatedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        LogCatalogChange(logger, @event.ProductId, @event.Name);
        return Task.CompletedTask;
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "Product catalog changed: {ProductId} ({Name}).")]
    private static partial void LogCatalogChange(ILogger logger, DefaultIdType productId, string name);
}
