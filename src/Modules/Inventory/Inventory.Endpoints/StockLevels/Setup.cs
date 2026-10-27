using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Inventory.Endpoints.StockLevels;

public static class Setup
{
    public static void MapStockLevelsEndpoints(this RouteGroupBuilder versionedApiGroup)
    {
        var v1StockLevelsApiGroup = versionedApiGroup
            .MapGroup("/stock-levels")
            .WithTags("StockLevels")
            .MapToApiVersion(1);

        v1.Receive.Endpoint.MapEndpoint(v1StockLevelsApiGroup);
        v1.Adjust.Endpoint.MapEndpoint(v1StockLevelsApiGroup);
        v1.ChangeLowStockThreshold.Endpoint.MapEndpoint(v1StockLevelsApiGroup);
        v1.Movements.Endpoint.MapEndpoint(v1StockLevelsApiGroup);
    }
}
