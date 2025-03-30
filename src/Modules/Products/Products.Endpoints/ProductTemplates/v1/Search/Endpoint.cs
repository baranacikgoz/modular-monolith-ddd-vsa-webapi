using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Common.Application.Queries.Pagination;
using Products.Application.ProductTemplates.DTOs;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using MediatR;
using Products.Application.ProductTemplates.Features.Search;

namespace Products.Endpoints.ProductTemplates.v1.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productTemplatesApiGroup)
    {
        productTemplatesApiGroup
            .MapGet("search", SearchProductTemplatesAsync)
            .WithDescription("Search product templates.")
            .MustHavePermission(CustomActions.Search, CustomResources.ProductTemplates)
            .Produces<PaginationResult<ProductTemplateDto>>(StatusCodes.Status200OK)
            .TransformResultTo<PaginationResult<ProductTemplateDto>>();
    }

    private static async Task<Result<PaginationResult<ProductTemplateDto>>> SearchProductTemplatesAsync(
        [AsParameters] Request request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender.Send(new SearchProductTemplatesQuery
        {
            Brand = request.Brand,
            Model = request.Model,
            Color = request.Color,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            OrderBy = null,
            OrderByDescending = x => x.CreatedOn
        }, cancellationToken);
}
