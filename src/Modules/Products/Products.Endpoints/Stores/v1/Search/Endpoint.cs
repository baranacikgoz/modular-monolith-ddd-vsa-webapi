using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Pagination;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;

namespace Products.Endpoints.Stores.v1.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder storesApiGroup)
    {
        storesApiGroup
            .MapGet("search", SearchStoresAsync)
            .WithDescription("Search stores.")
            .MustHavePermission(CustomActions.Search, CustomResources.Stores)
            .Produces<PaginationResponse<Response>>()
            .TransformResultTo<PaginationResponse<Response>>();
    }

    private static async Task<Result<PaginationResponse<Response>>> SearchStoresAsync(
        [AsParameters] Request request,
        [FromServices] IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .Stores
            .AsNoTracking()
            .TagWith(nameof(SearchStoresAsync))
            .WhereIf(s => EF.Functions.ILike(s.Name, $"%{request.Name}%"), !string.IsNullOrWhiteSpace(request.Name))
            .WhereIf(s => EF.Functions.ILike(s.Description, $"%{request.Description}%"),
                !string.IsNullOrWhiteSpace(request.Description))
            .WhereIf(s => EF.Functions.ILike(s.Address, $"%{request.Address}%"),
                !string.IsNullOrWhiteSpace(request.Address))
            .PaginateAsync(
                request: request,
                selector: s => new Response
                {
                    Id = s.Id,
                    OwnerId = s.OwnerId,
                    Name = s.Name,
                    Description = s.Description,
                    Address = s.Address,
                    ProductCount = s.Products.Count,
                    CreatedBy = s.CreatedBy,
                    CreatedOn = s.CreatedOn,
                    LastModifiedBy = s.LastModifiedBy,
                    LastModifiedOn = s.LastModifiedOn
                },
                cancellationToken: cancellationToken);
    }
}
