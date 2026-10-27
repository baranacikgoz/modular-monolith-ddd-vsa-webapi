using Inventory.Domain.WarehouseConnections;

namespace Inventory.Endpoints.StockReservations.v1.WebhookCallback;

/// <summary>The one place the callback route is spelled, so the address handed to the warehouse system matches the mapped route.</summary>
internal static class WebhookRoute
{
    internal const string ConnectionIdRouteKey = "connectionId";

    internal const string Template = "webhooks/warehouse/{" + ConnectionIdRouteKey + "}";

    internal static string PathFor(WarehouseConnectionId connectionId)
    {
        return $"/v1/stock-reservations/webhooks/warehouse/{connectionId}";
    }
}
