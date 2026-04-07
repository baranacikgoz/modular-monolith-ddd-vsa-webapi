using Common.Application.AuditLog;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Pagination;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Products.Application.Persistence;
using Products.Domain.Products;

namespace Products.Endpoints.Products.v1.EventHistory;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productsApiGroup)
    {
        productsApiGroup
            .MapGet("{id}/audit-log", GetProductAuditLogAsync)
            .WithDescription("Get product audit log.")
            .MustHavePermission(CustomActions.Read, CustomResources.Products)
            .Produces<PaginationResponse<AuditLogDto>>()
            .TransformResultTo<PaginationResponse<AuditLogDto>>();
    }

    private static async Task<Result<PaginationResponse<AuditLogDto>>> GetProductAuditLogAsync(
        [AsParameters] Request request,
        IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .AuditLog
            .GetAuditLogAsync<Product, ProductId>(
                request.Id,
                request,
                cancellationToken);
    }
}
