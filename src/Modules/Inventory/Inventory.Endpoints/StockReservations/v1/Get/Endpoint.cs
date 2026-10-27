using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Endpoints.StockReservations.v1.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder stockReservationsApiGroup)
    {
        stockReservationsApiGroup
            .MapGet("{id}", GetStockReservationAsync)
            .WithDescription("Get any stock reservation by id. Administration only; a store owner reads the reservations of their own " +
                             "stores through /my and /my/{id}.")
            .RequireScope(KeycloakScopes.StockReservations.ViewAll)
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static Task<Result<Response>> GetStockReservationAsync(
        [AsParameters] Request request,
        IInventoryDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return ReadAsync(dbContext.StockReservations, request.Id, cancellationToken);
    }

    /// <summary>
    ///     The reservation <paramref name="id" /> names, looked up inside <paramref name="reservations" />: every reservation for the
    ///     administration route, the caller's own for the store owner's route, where one outside the set is simply not found.
    /// </summary>
    internal static async Task<Result<Response>> ReadAsync(
        IQueryable<StockReservation> reservations, StockReservationId id, CancellationToken cancellationToken)
    {
        return await reservations
            .AsNoTracking()
            .TagWith(nameof(ReadAsync), id)
            .Where(r => r.Id == id)
            .Select(r => new Response
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
            })
            .SingleAsResultAsync(nameof(StockReservation), cancellationToken);
    }
}
