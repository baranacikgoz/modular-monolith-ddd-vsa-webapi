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
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.EventHistory;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder storeProductsApiGroup)
    {
        storeProductsApiGroup
            .MapGet("{id}/audit-log", GetStoreAuditLogAsync)
            .WithDescription("Get store's audit log.")
            .MustHavePermission(CustomActions.Read, CustomResources.Stores)
            .Produces<PaginationResponse<AuditLogDto>>()
            .TransformResultTo<PaginationResponse<AuditLogDto>>();
    }

    private static async Task<Result<PaginationResponse<AuditLogDto>>> GetStoreAuditLogAsync(
        [AsParameters] Request request,
        IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .AuditLog
            .GetAuditLogAsync<Store, StoreId>(
                request.Id,
                request,
                cancellationToken);
    }
}
