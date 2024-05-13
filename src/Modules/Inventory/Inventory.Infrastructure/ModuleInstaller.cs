using Inventory.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Inventory.Application.Stores;
using Inventory.Application.Products;

namespace Inventory.Infrastructure;

public static class ModuleInstaller
{
    public static IServiceCollection AddInventoryModule(this IServiceCollection services)
        => services
            .AddPersistence();

    public static WebApplication UseInventoryModule(
        this WebApplication app,
        RouteGroupBuilder versionedApiGroup)
    {
        app.UsePersistence();

        versionedApiGroup.MapStoresEndpoints();
        versionedApiGroup.MapProductsEndpoints();

        return app;
    }
}
