using Common.Application.EventBus;
using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.Application.Storage;
using Common.IntegrationEvents;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Products.Application.Persistence;
using ZiggyCreatures.Caching.Fusion;

namespace Products.Application.IntegrationEventHandlers;

/// <summary>
///     Deletes the public-store objects of a removed product's images. Deleting a key that is already gone is not an error
///     (the store's delete is idempotent), so a redelivery the cache pre-filter and the inbox both missed is harmless. A
///     storage failure is not caught: the exception reaches the bus, which retries the message, so no object is given up on.
/// </summary>
public sealed class DeleteOrphanedProductImageObjectsHandler(
    IPublicObjectStore objectStore,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    IInboxStore<IProductsDbContext> inbox,
    ILogger<DeleteOrphanedProductImageObjectsHandler> logger
) : IntegrationEventHandlerBase<ProductImageObjectsOrphanedIntegrationEvent>(cache, cachingOptions, inbox, logger)
{
    protected override Task ProcessAsync(ProductImageObjectsOrphanedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        return objectStore.DeleteManyAsync(@event.ObjectKeys.ToList(), cancellationToken);
    }
}
