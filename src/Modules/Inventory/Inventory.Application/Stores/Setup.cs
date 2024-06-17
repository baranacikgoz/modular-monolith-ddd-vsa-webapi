using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Inventory.Application.Stores;
public static class Setup
{
    public static void MapStoresEndpoints(this RouteGroupBuilder versionedApiGroup)
    {
        var v1StoresApiGroup = versionedApiGroup
            .MapGroup("/stores")
            .WithTags("Stores")
            .MapToApiVersion(1);

        var v1MyStoresApiGroup = v1StoresApiGroup.MapGroup("/my");
        v1.My.Create.Endpoint.MapEndpoint(v1MyStoresApiGroup);
        v1.My.Update.Endpoint.MapEndpoint(v1MyStoresApiGroup);
        v1.My.Get.Endpoint.MapEndpoint(v1MyStoresApiGroup);

        var v1MyStoreProductsApiGroup = v1MyStoresApiGroup.MapGroup("/products");
        v1.My.Products.Add.Endpoint.MapEndpoint(v1MyStoreProductsApiGroup);
        v1.My.Products.Update.Endpoint.MapEndpoint(v1MyStoreProductsApiGroup);
        v1.My.Products.Get.Endpoint.MapEndpoint(v1MyStoreProductsApiGroup);

        // v2StoresApiGroup = versionedApiGroup
        //     .MapGroup("/stores")
        //     .WithTags("Stores")
        //     .MapToApiVersion(2)

        // v2.xxx.Endpoint.MapEndpoint(v2StoresApiGroup)
    }
}
