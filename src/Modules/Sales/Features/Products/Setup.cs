using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Sales.Features.Products.Infrastructure;

namespace Sales.Features.Products;

internal static class Setup
{
    public static IServiceCollection AddProductsFeature(this IServiceCollection services)
        => services
            .AddProductsInfrastructure();

    public static void MapProductsEndpoints(this RouteGroupBuilder versionedApiGroup)
    {
        var v1ProductsApiGroup = versionedApiGroup
            .MapGroup("/products")
            .WithTags("Products")
            .MapToApiVersion(1);

        Products.UseCases.v1.Create.Endpoint.MapEndpoint(v1ProductsApiGroup);

        // v2ProductsApiGroup = versionedApiGroup
        //     .MapGroup("/products")
        //     .WithTags("Products")
        //     .MapToApiVersion(2)

        // Products.UseCases.v2.xxx.Endpoint.MapEndpoint(v2ProductsApiGroup)
    }
}
