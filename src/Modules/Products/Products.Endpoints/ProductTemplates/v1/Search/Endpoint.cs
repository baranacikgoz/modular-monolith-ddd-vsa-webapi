using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Products.Application.ProductTemplates.DTOs;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Common.Application.Pagination;
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
            .Produces<PaginationResponse<ProductTemplateResponse>>(StatusCodes.Status200OK)
            .TransformResultTo<PaginationResponse<ProductTemplateResponse>>();
    }

    private static async Task<Result<PaginationResponse<ProductTemplateResponse>>> SearchProductTemplatesAsync(
        [AsParameters] Request request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender.Send(new SearchProductTemplatesRequest
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
