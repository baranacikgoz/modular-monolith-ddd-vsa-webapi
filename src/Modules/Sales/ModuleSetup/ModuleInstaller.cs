using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Sales.Features.Products;
using Sales.Features.Stores;
using Sales.Persistence;

namespace Sales.ModuleSetup;

public static class ModuleInstaller
{
    public static IServiceCollection AddSalesModule(this IServiceCollection services)
        => services
            .AddPersistence()
            .AddStoresFeature()
            .AddProductsFeature();

    public static WebApplication UseSalesModule(
        this WebApplication app,
        RouteGroupBuilder versionedApiGroup)
    {
        app.UsePersistence();

        versionedApiGroup.MapStoresEndpoints();
        versionedApiGroup.MapProductsEndpoints();

        return app;
    }
}
