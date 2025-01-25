using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Products.Endpoints.Products;

public static class Setup
{
    public static void MapProductsEndpoints(this RouteGroupBuilder versionedApiGroup)
    {
        var v1ProductsApiGroup = versionedApiGroup
            .MapGroup("/products")
            .WithTags("Products")
            .MapToApiVersion(1);

        v1.Get.Endpoint.MapEndpoint(v1ProductsApiGroup);
        v1.Update.Endpoint.MapEndpoint(v1ProductsApiGroup);
        v1.Search.Endpoint.MapEndpoint(v1ProductsApiGroup);
        v1.My.Update.Endpoint.MapEndpoint(v1ProductsApiGroup);
        v1.My.Search.Endpoint.MapEndpoint(v1ProductsApiGroup);
        v1.My.Get.Endpoint.MapEndpoint(v1ProductsApiGroup);

        // v2StoresApiGroup = versionedApiGroup
        //     .MapGroup("/stores")
        //     .WithTags("Stores")
        //     .MapToApiVersion(2)

        // v2.xxx.Endpoint.MapEndpoint(v2StoresApiGroup)
    }
}
