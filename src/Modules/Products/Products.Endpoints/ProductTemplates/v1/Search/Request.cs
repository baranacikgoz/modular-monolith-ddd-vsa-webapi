using Common.Application.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Products.Endpoints.ProductTemplates.v1.Search;

public sealed record Request : PaginationRequest
{
    [FromQuery] public string? Brand { get; init; }

    [FromQuery] public string? Model { get; init; }

    [FromQuery] public string? Color { get; init; }
}
