using Common.Application.Pagination;
using Inventory.Domain.StockLevels;

namespace Inventory.Endpoints.StockLevels.v1.Search;

/// <summary>
///     One stock level with what the local product copy knows about its product. <see cref="ProductName" /> and
///     <see cref="StoreId" /> are null until the product has been projected into Inventory.
/// </summary>
public sealed record Response
{
    public required StockLevelId Id { get; init; }
    public required DefaultIdType ProductId { get; init; }
    public string? ProductName { get; init; }
    public DefaultIdType? StoreId { get; init; }
    public required int QuantityOnHand { get; init; }
    public required int LowStockThreshold { get; init; }
}

/// <summary>
///     The shared page shape plus the one signal specific to the product name filter: <see cref="IsTruncated" /> is true
///     when Products cut its match list at its cap, so the page may be missing stock levels of products it did not return.
/// </summary>
public sealed record PageResponse(ICollection<Response> Data, int TotalCount, int PageNumber, int PageSize, bool IsTruncated)
    : PaginationResponse<Response>(Data, TotalCount, PageNumber, PageSize);
