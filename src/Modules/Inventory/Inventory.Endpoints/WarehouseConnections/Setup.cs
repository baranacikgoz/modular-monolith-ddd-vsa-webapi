using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Inventory.Endpoints.WarehouseConnections;

public static class Setup
{
    public static void MapWarehouseConnectionsEndpoints(this RouteGroupBuilder versionedApiGroup)
    {
        var v1WarehouseConnectionsApiGroup = versionedApiGroup
            .MapGroup("/warehouse-connections")
            .WithTags("WarehouseConnections")
            .MapToApiVersion(1);

        v1.Create.Endpoint.MapEndpoint(v1WarehouseConnectionsApiGroup);
        v1.ReplaceCredentials.Endpoint.MapEndpoint(v1WarehouseConnectionsApiGroup);
        v1.RotateWebhookSecret.Endpoint.MapEndpoint(v1WarehouseConnectionsApiGroup);
        v1.Deactivate.Endpoint.MapEndpoint(v1WarehouseConnectionsApiGroup);
        v1.Get.Endpoint.MapEndpoint(v1WarehouseConnectionsApiGroup);
        v1.Search.Endpoint.MapEndpoint(v1WarehouseConnectionsApiGroup);
        v1.My.Search.Endpoint.MapEndpoint(v1WarehouseConnectionsApiGroup);
    }
}
