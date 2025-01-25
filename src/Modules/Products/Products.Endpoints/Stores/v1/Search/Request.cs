using Common.Endpoints.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Products.Endpoints.Stores.v1.Search;

public sealed record Request : PaginationRequest
{
    [FromQuery]
    public string? Name { get; init; }

    [FromQuery]
    public string? Description { get; init; }

    [FromQuery]
    public string? Address { get; init; }

    public Request() { }
}
