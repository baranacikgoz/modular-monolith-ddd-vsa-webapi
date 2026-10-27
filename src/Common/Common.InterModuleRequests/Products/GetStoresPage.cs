using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.Products;

/// <summary>
///     Keyset page of every store with the access facts a read model in another module keeps. Used only by that module's
///     nightly projection reconciliation, never on a request path: the request path reads the local read model. The walk
///     is newest first (StoreId descending): <paramref name="AfterStoreId" /> null starts from the newest store,
///     otherwise the page holds the stores older than it; the response's
///     <see cref="GetStoresPageResponse.NextAfterStoreId" /> is null on the last page.
/// </summary>
public sealed record GetStoresPageRequest(DefaultIdType? AfterStoreId, int PageSize)
    : IInterModuleRequest<GetStoresPageResponse>;

public sealed record GetStoresPageResponse(
    IReadOnlyList<StorePageItem> Stores,
    DefaultIdType? NextAfterStoreId);

/// <summary>Version is the Store aggregate version, the same number <c>StoreAccessChangedIntegrationEvent</c> carries.</summary>
public sealed record StorePageItem(
    DefaultIdType StoreId,
    ApplicationUserId OwnerId,
    bool IsActive,
    long Version);
