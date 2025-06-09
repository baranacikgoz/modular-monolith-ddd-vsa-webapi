using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Common.Application.Pagination;
using Common.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;

namespace Products.Endpoints.ProductTemplates.v1.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productTemplatesApiGroup)
    {
        productTemplatesApiGroup
            .MapGet("search", SearchProductTemplatesAsync)
            .WithDescription("Search product templates.")
            .MustHavePermission(CustomActions.Search, CustomResources.ProductTemplates)
            .Produces<PaginationResponse<Response>>(StatusCodes.Status200OK)
            .TransformResultTo<PaginationResponse<Response>>();
    }

    private static async Task<Result<PaginationResponse<Response>>> SearchProductTemplatesAsync(
        [AsParameters] Request request,
        [FromServices] IProductsDbContext dbContext,
        CancellationToken cancellationToken)
        => await dbContext
            .ProductTemplates
            .AsNoTracking()
            .TagWith(nameof(SearchProductTemplatesAsync))
            .WhereIf(p => EF.Functions.ILike(p.Brand, $"%{request.Brand}%"), condition: !string.IsNullOrWhiteSpace(request.Brand))
            .WhereIf(p => EF.Functions.ILike(p.Model, $"%{request.Model}%"), condition: !string.IsNullOrWhiteSpace(request.Model))
            .WhereIf(p => EF.Functions.ILike(p.Color, $"%{request.Color}%"), condition: !string.IsNullOrWhiteSpace(request.Color))
            .PaginateAsync(
                request: request,
                selector: p => new Response
                {
                    Id = p.Id,
                    Brand = p.Brand,
                    Model = p.Model,
                    Color = p.Color,
                    CreatedBy = p.CreatedBy,
                    CreatedOn = p.CreatedOn,
                    LastModifiedBy = p.LastModifiedBy,
                    LastModifiedOn = p.LastModifiedOn,
                },
                cancellationToken: cancellationToken);
}
