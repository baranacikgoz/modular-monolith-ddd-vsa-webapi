using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Pagination;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Inventory.Application.Persistence;
using Inventory.Application.StockReservations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using AllReservations = Inventory.Endpoints.StockReservations.v1.Get;

namespace Inventory.Endpoints.StockReservations.v1.My.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder stockReservationsApiGroup)
    {
        stockReservationsApiGroup
            .MapGet("my", SearchMyStockReservationsAsync)
            .WithDescription("List the reservations of the products of the active stores I own, newest first, optionally of one product " +
                             "or one status. Pass the previous page's NextCursor as 'after' to continue without an offset. Reads " +
                             "Inventory's own copy of store access, so a store that is not active, or one nobody has told Inventory " +
                             "about yet, contributes nothing; a caller who owns no active store gets an empty page.")
            .RequireScope(KeycloakScopes.StockReservations.ViewOwn)
            .Produces<PaginationResponse<AllReservations.Response>>()
            .TransformResultTo<PaginationResponse<AllReservations.Response>>();
    }

    private static async Task<Result<PaginationResponse<AllReservations.Response>>> SearchMyStockReservationsAsync(
        [AsParameters] Request request,
        ICurrentUser currentUser,
        IInventoryDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .StockReservationsOfActiveStoresOwnedBy(currentUser.Id)
            .AsNoTracking()
            .TagWith(nameof(SearchMyStockReservationsAsync), currentUser.Id)
            .WhereIf(r => r.ProductId == request.ProductId, request.ProductId is not null)
            .WhereIf(r => r.Status == request.Status, request.Status is not null)
            .PaginateAsync(
                request: request,
                selector: r => new AllReservations.Response
                {
                    Id = r.Id,
                    ProductId = r.ProductId,
                    Quantity = r.Quantity,
                    Status = r.Status,
                    ReservationDeadline = r.ReservationDeadline,
                    ProviderReference = r.ProviderReference,
                    IsReadOnly = r.IsReadOnly,
                    HasPendingReleaseAttempt = r.LastReleaseAttemptReference != null,
                    ProviderStatus = r.ProviderStatus
                },
                orderByDescending: r => r.CreatedOn,
                tiebreaker: r => r.Id,
                cancellationToken: cancellationToken);
    }
}
