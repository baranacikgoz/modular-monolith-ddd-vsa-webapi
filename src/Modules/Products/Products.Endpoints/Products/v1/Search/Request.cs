using Common.Endpoints.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Products.Endpoints.Products.v1.Search;

public sealed record Request : PaginationRequest
{
    [FromQuery]
    public string? Name { get; init; }

    [FromQuery]
    public string? Description { get; init; }

    [FromQuery]
    public int? MinQuantity { get; init; }

    [FromQuery]
    public int? MaxQuantity { get; init; }

    [FromQuery]
    public decimal? MinPrice { get; init; }

    [FromQuery]
    public decimal? MaxPrice { get; init; }

    public Request() { }
}
