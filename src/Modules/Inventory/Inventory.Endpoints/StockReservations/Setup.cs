using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Inventory.Endpoints.StockReservations;

public static class Setup
{
    public static void MapStockReservationsEndpoints(this RouteGroupBuilder versionedApiGroup)
    {
        var v1StockReservationsApiGroup = versionedApiGroup
            .MapGroup("/stock-reservations")
            .WithTags("StockReservations")
            .MapToApiVersion(1);

        v1.Reserve.Endpoint.MapEndpoint(v1StockReservationsApiGroup);
        v1.Commit.Endpoint.MapEndpoint(v1StockReservationsApiGroup);
        v1.Release.Endpoint.MapEndpoint(v1StockReservationsApiGroup);
        v1.Get.Endpoint.MapEndpoint(v1StockReservationsApiGroup);
        v1.ReserveSeries.Endpoint.MapEndpoint(v1StockReservationsApiGroup);
        v1.WebhookCallback.Endpoint.MapEndpoint(v1StockReservationsApiGroup);
    }
}
