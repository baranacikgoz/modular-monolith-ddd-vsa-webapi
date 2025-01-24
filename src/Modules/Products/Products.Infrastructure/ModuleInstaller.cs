using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Products.Endpoints.Products;
using Products.Endpoints.ProductTemplates;
using Products.Endpoints.Stores;
using Products.Infrastructure.Persistence;

namespace Products.Infrastructure;

public static class ModuleInstaller
{
    public static IServiceCollection AddProductsModule(this IServiceCollection services)
        => services
            .AddPersistence();

    public static WebApplication UseProductsModule(
        this WebApplication app,
        RouteGroupBuilder versionedApiGroup)
    {
        app.UsePersistence();

        versionedApiGroup.MapStoresEndpoints();
        versionedApiGroup.MapProductsEndpoints();
        versionedApiGroup.MapProductTemplatesEndpoints();

        return app;
    }
}
