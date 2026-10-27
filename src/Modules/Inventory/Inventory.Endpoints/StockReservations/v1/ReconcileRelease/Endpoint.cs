using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Inventory.Infrastructure.StockReservations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Inventory.Endpoints.StockReservations.v1.ReconcileRelease;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder stockReservationsApiGroup)
    {
        stockReservationsApiGroup
            .MapPost("{id}/reconcile-release", ReconcileReleaseAsync)
            .WithDescription(
                "Resolves a release whose outcome is unknown, one the warehouse system did not answer in time. The warehouse " +
                "system is asked, through the connection of the product's store, what became of the release: if it released the " +
                "hold, the release is completed here; if it never did, the attempt marker is dropped so the release can be tried " +
                "again. When the warehouse still does not answer, the marker stays and the call can be repeated.")
            .RequireScope(KeycloakScopes.StockReservations.Update)
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static Task<Result<Response>> ReconcileReleaseAsync(
        [AsParameters] Request request,
        StockReleaseReconciler reconciler,
        CancellationToken cancellationToken)
    {
        return reconciler
            .ReconcileAsync(request.Id, cancellationToken)
            .MapAsync(outcome => new Response { Outcome = outcome });
    }
}
