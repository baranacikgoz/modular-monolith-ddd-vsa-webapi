using Common.Application.Localization;
using Common.Application.Queries.Pagination;
using Common.Endpoints.Pagination;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Products.Endpoints.Products.v1.My.Search;

public sealed record Request : PaginationRequest
{
    [FromQuery(Name = "name")]
    public string? Name { get; init; }

    [FromQuery(Name = "description")]
    public string? Description { get; init; }

    [FromQuery(Name = "minQuantity")]
    public int? MinQuantity { get; init; }

    [FromQuery(Name = "maxQuantity")]
    public int? MaxQuantity { get; init; }

    [FromQuery(Name = "minPrice")]
    public decimal? MinPrice { get; init; }

    [FromQuery(Name = "maxPrice")]
    public decimal? MaxPrice { get; init; }
    public Request() { }
}
