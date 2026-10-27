using Common.Application.Options;
using Common.Endpoints.Versioning;
using Common.Infrastructure.Modules;
using Inventory.Endpoints.StockLevels;
using Inventory.Endpoints.StockReservations;
using Inventory.Endpoints.WarehouseConnections;
using Inventory.Infrastructure.BulkAdjustments;
using Inventory.Infrastructure.Gateway;
using Inventory.Infrastructure.Persistence;
using Inventory.Infrastructure.ProductSummaries;
using Inventory.Infrastructure.RateLimiting;
using Inventory.Infrastructure.StockPushes;
using Inventory.Infrastructure.StockReservations;
using Inventory.Infrastructure.StoreAccesses;
using Inventory.Infrastructure.Telemetry;
using Inventory.Infrastructure.Webhooks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace Inventory.Endpoints;

public sealed class InventoryModule : IModule
{
    public string Name => "Inventory";
    public int StartupPriority => 5;

    public IEnumerable<string> ActivitySourceNames => [InventoryTelemetry.ActivitySourceName];

    public IEnumerable<string> MeterNames => [InventoryTelemetry.MeterName];

    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence();
        services.AddStockReservationExpirySweep();
        services.AddStockReleaseReconciliation();
        services.AddProductSummaryReconciliation();
        services.AddStoreAccessReconciliation();
        services.AddWarehouseGatewayInfrastructure();
        services.AddWarehouseWebhooks();
        services.AddStockPushes();
        services.AddStockBulkAdjustments();
    }

    public void UseModule(IApplicationBuilder app)
    {
        app.UsePersistence();
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var apiVersionSet = endpoints.GetApiVersionSet();
        var versionedApiGroup = endpoints
            .MapGroup("/v{version:apiVersion}")
            .AddFluentValidationAutoValidation()
            .WithApiVersionSet(apiVersionSet)
            .RequireAuthorization();

        versionedApiGroup.MapStockReservationsEndpoints();
        versionedApiGroup.MapStockLevelsEndpoints();
        versionedApiGroup.MapWarehouseConnectionsEndpoints();
    }

    public IEnumerable<Action<RateLimiterOptions, CustomRateLimitingOptions>>? RateLimitingPolicies => Policies.Get();
}
