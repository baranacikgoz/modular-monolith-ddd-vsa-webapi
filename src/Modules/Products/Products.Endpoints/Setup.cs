using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Products.Endpoints.Products;
using Products.Endpoints.ProductTemplates;
using Products.Endpoints.Stores;

namespace Products.Endpoints;

public static class Setup
{
    public static WebApplication MapProductsModuleEndpoints(this WebApplication app, RouteGroupBuilder versionedApiGroup)
    {
        versionedApiGroup.MapStoresEndpoints();
        versionedApiGroup.MapProductsEndpoints();
        versionedApiGroup.MapProductTemplatesEndpoints();

        return app;
    }
}
