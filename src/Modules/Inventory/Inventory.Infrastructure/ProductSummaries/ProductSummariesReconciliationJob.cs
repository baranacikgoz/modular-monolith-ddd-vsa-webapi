using Common.Application.Options;
using Common.Application.Persistence.Projections;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Products;
using Inventory.Application.Persistence;
using Inventory.Application.ProductSummaries;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.ProductSummaries;

/// <summary>Keyset cursor over Products' product list, walked newest first: the last (oldest so far) product id of the previous page.</summary>
public sealed record ProductSummariesReconciliationCursor(DefaultIdType AfterProductId);

/// <summary>
///     Nightly heal of <see cref="ProductSummary" /> from Products, page by page through
///     <see cref="GetProductsPageRequest" />: the one place Inventory asks Products for every product, never on a
///     request path. It creates rows the events never delivered (products that predate the read model) and corrects a
///     name or store a lost event left behind, each guarded by the product's version.
/// </summary>
public sealed class ProductSummariesReconciliationJob(
    IInventoryDbContext dbContext,
    IInterModuleRequestClient<GetProductsPageRequest, GetProductsPageResponse> productsClient,
    IOptions<ProjectionReconciliationOptions> optionsProvider,
    ILogger<ProductSummariesReconciliationJob> logger)
    : ProjectionReconciliationJob<ProductPageItem, ProductSummariesReconciliationCursor>(dbContext, optionsProvider, logger)
{
    protected override async Task<(IReadOnlyList<ProductPageItem> Items, ProductSummariesReconciliationCursor? Next)> FetchPageAsync(
        ProductSummariesReconciliationCursor? cursor, int pageSize, CancellationToken cancellationToken)
    {
        var response = await productsClient.SendAsync(new GetProductsPageRequest(cursor?.AfterProductId, pageSize), cancellationToken);

        return (response.Products,
            response.NextAfterProductId is { } next ? new ProductSummariesReconciliationCursor(next) : null);
    }

    protected override async Task<int> ApplyPageAsync(IReadOnlyList<ProductPageItem> items, CancellationToken cancellationToken)
    {
        var healed = 0;
        foreach (var source in items)
        {
            // FindAsync, not a query: it also sees a row added earlier in this same unsaved page, which a SELECT cannot.
            var row = await dbContext.ProductSummaries.FindAsync([source.ProductId], cancellationToken);
            var created = row is null;
            if (row is null)
            {
                row = ProductSummary.Create(source.ProductId);
                dbContext.ProductSummaries.Add(row);
            }

            var changed = row.ApplyIfNewer(source.StoreId, source.Name, source.Version);
            if (changed || created)
            {
                healed++;
            }
        }

        return healed;
    }
}
