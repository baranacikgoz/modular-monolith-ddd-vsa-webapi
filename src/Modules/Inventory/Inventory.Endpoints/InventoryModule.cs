using Common.Endpoints.Versioning;
using Common.Infrastructure.Modules;
using Inventory.Endpoints.StockReservations;
using Inventory.Infrastructure.Gateway;
using Inventory.Infrastructure.Persistence;
using Inventory.Infrastructure.StockReservations;
using Inventory.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
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
        services.AddWarehouseGatewayInfrastructure(configuration);
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
    }
}
