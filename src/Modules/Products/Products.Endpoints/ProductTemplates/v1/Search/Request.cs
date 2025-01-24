using Common.Endpoints.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Products.Endpoints.ProductTemplates.v1.Search;

public sealed record Request : PaginationRequest
{
    [FromQuery(Name = "brand")]
    public string? Brand { get; init; }

    [FromQuery(Name = "model")]
    public string? Model { get; init; }

    [FromQuery(Name = "color")]
    public string? Color { get; init; }

    public Request() { }
}
