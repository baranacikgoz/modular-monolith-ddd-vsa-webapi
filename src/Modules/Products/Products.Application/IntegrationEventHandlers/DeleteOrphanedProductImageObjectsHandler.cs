using Common.Application.EventBus;
using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.Application.Storage;
using Common.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Products.Application.Persistence;
using Products.Domain.Products;
using ZiggyCreatures.Caching.Fusion;

namespace Products.Application.IntegrationEventHandlers;

/// <summary>
///     Deletes the public-store objects of images that were removed: a whole product's images, or one image taken off a
///     product. An image's key is a hash of its bytes scoped to the product, so the same file added again after the removal
///     reuses the key; a key some image row points at by the time the message is consumed is not an orphan and stays.
///     Deleting a key that is already gone is not an error (the store's delete is idempotent), so a redelivery the cache
///     pre-filter and the inbox both missed is harmless. A storage failure is not caught: the exception reaches the bus,
///     which retries the message, so no object is given up on.
/// </summary>
public sealed class DeleteOrphanedProductImageObjectsHandler(
    IPublicObjectStore objectStore,
    IProductsDbContext dbContext,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    IInboxStore<IProductsDbContext> inbox,
    ILogger<DeleteOrphanedProductImageObjectsHandler> logger
) : IntegrationEventHandlerBase<ProductImageObjectsOrphanedIntegrationEvent>(cache, cachingOptions, inbox, logger)
{
    protected override async Task ProcessAsync(ProductImageObjectsOrphanedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        var keys = @event.ObjectKeys.ToList();

        var stillReferenced = await dbContext.Set<ProductImage>()
            .AsNoTracking()
            .TagWith(nameof(ProcessAsync))
            .Where(image => keys.Contains(image.ObjectKey))
            .Select(image => image.ObjectKey)
            .ToListAsync(cancellationToken);

        var orphaned = keys.Except(stillReferenced, StringComparer.Ordinal).ToList();
        if (orphaned.Count > 0)
        {
            await objectStore.DeleteManyAsync(orphaned, cancellationToken);
        }
    }
}
