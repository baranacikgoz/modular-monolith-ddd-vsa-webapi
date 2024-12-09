using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Products;
using Products.Application.StoreProducts;
using Products.Application.Stores;
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
        versionedApiGroup.MapStoreProductsEndpoints();

        return app;
    }
}
