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

        var v1MyProductsApiGroup = v1ProductsApiGroup.MapGroup("/my");
        UseCases.v1.My.Create.Endpoint.MapEndpoint(v1MyProductsApiGroup);
        UseCases.v1.My.UpdatePrice.Endpoint.MapEndpoint(v1MyProductsApiGroup);

        // v2ProductsApiGroup = versionedApiGroup
        //     .MapGroup("/products")
        //     .WithTags("Products")
        //     .MapToApiVersion(2)

        // Products.UseCases.v2.xxx.Endpoint.MapEndpoint(v2ProductsApiGroup)
    }
}
