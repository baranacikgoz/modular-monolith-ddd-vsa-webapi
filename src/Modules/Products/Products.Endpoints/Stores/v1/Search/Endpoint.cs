using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Products.Application.Stores.DTOs;
using Products.Application.Stores.Features.Search;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Common.Application.Pagination;
using Common.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Products.Infrastructure.Persistence;

namespace Products.Endpoints.Stores.v1.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder storesApiGroup)
    {
        storesApiGroup
            .MapGet("search", SearchStoresAsync)
            .WithDescription("Search stores.")
            .MustHavePermission(CustomActions.Search, CustomResources.Stores)
            .Produces<PaginationResponse<StoreResponse>>(StatusCodes.Status200OK)
            .TransformResultTo<PaginationResponse<StoreResponse>>();
    }

    private static async Task<Result<PaginationResponse<StoreResponse>>> SearchStoresAsync(
        [AsParameters] Request request,
        [FromServices] ProductsDbContext dbContext,
        CancellationToken cancellationToken)
        => await dbContext
            .Stores
            .AsNoTracking()
            .TagWith(nameof(SearchStoresRequest))
            .WhereIf(s => EF.Functions.ILike(s.Name, $"%{request.Name}%"), condition: !string.IsNullOrWhiteSpace(request.Name))
            .WhereIf(s => EF.Functions.ILike(s.Description, $"%{request.Description}%"), condition: !string.IsNullOrWhiteSpace(request.Description))
            .WhereIf(s => EF.Functions.ILike(s.Address, $"%{request.Address}%"), condition: !string.IsNullOrWhiteSpace(request.Address))
            .PaginateAsync(
                request: request,
                selector: s => new StoreResponse
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
