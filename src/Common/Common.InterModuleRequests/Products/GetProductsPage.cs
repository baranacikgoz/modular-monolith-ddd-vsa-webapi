using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.Products;

/// <summary>
///     Keyset page of every product with the fields a read model in another module keeps. Used only by that module's
///     nightly projection reconciliation, never on a request path: the request path reads the local read model. The walk
///     is newest first (ProductId descending): <paramref name="AfterProductId" /> null starts from the newest product,
///     otherwise the page holds the products older than it; the response's
///     <see cref="GetProductsPageResponse.NextAfterProductId" /> is null on the last page.
/// </summary>
public sealed record GetProductsPageRequest(DefaultIdType? AfterProductId, int PageSize)
    : IInterModuleRequest<GetProductsPageResponse>;

public sealed record GetProductsPageResponse(
    IReadOnlyList<ProductPageItem> Products,
    DefaultIdType? NextAfterProductId);

/// <summary>Version is the Product aggregate version, the same number <c>ProductChangedIntegrationEvent</c> carries.</summary>
public sealed record ProductPageItem(
    DefaultIdType ProductId,
    DefaultIdType StoreId,
    string Name,
    long Version);
