using Common.Endpoints.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Products.Endpoints.Stores.v1.Search;

public sealed record Request : PaginationRequest
{
    [FromQuery(Name = "name")]
    public string? Name { get; init; }

    [FromQuery(Name = "description")]
    public string? Description { get; init; }

    [FromQuery(Name = "address")]
    public string? Address { get; init; }

    public Request() { }
}
