using Inventory.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Inventory.Application.Stores;
using Inventory.Application.Products;
using Inventory.Application.StoreProducts;

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
        versionedApiGroup.MapStoreProductsEndpoints();

        return app;
    }
}
