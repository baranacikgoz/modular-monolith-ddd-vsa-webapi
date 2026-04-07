using Common.Application.AuditLog;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Pagination;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.My.History;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder v1StoresApiGroup)
    {
        v1StoresApiGroup
            .MapGet("my/history", GetMyStoreAuditLogAsync)
            .WithDescription("Get my store's audit log.")
            .MustHavePermission(CustomActions.ReadMy, CustomResources.Stores)
            .Produces<PaginationResponse<AuditLogDto>>()
            .TransformResultTo<PaginationResponse<AuditLogDto>>();
    }

    private static async Task<Result<PaginationResponse<AuditLogDto>>> GetMyStoreAuditLogAsync(
        [AsParameters] Request request,
        ICurrentUser currentUser,
        IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .Stores
            .TagWith(nameof(GetMyStoreAuditLogAsync), currentUser.Id)
            .AsNoTracking()
            .Where(x => x.OwnerId == currentUser.Id)
            .Select(x => x.Id)
            .SingleAsResultAsync(resourceName: nameof(Store), cancellationToken)

            .BindAsync(async id => await dbContext
                .AuditLog
                .GetAuditLogAsync<Store, StoreId>(
                    id,
                    request,
                    cancellationToken));
    }
}
