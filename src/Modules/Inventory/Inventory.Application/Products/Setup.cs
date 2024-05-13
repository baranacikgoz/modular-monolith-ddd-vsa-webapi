using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Inventory.Application.Products;
public static class Setup
{
    public static void MapProductsEndpoints(this RouteGroupBuilder versionedApiGroup)
    {
        var v1ProductsApiGroup = versionedApiGroup
            .MapGroup("/products")
            .WithTags("Products")
            .MapToApiVersion(1);

        v1.Create.Endpoint.MapEndpoint(v1ProductsApiGroup);

        var v1MyProductsApiGroup = v1ProductsApiGroup.MapGroup("/my");

        v1.My.UpdatePrice.Endpoint.MapEndpoint(v1MyProductsApiGroup);

        // v2ProductsApiGroup = versionedApiGroup
        //     .MapGroup("/products")
        //     .WithTags("Products")
        //     .MapToApiVersion(2)

        // v2.xxx.Endpoint.MapEndpoint(v2ProductsApiGroup)
    }
}
