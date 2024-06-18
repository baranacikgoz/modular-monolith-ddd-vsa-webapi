using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Inventory.Application.StoreProducts;
public static class Setup
{
    public static void MapStoreProductsEndpoints(this RouteGroupBuilder versionedApiGroup)
    {
        var v1StoreProductsApiGroup = versionedApiGroup
            .MapGroup("/storeproducts")
            .WithTags("Store Products")
            .MapToApiVersion(1);

        v1.Search.Endpoint.MapEndpoint(v1StoreProductsApiGroup);

        var v1MyStoreProductsApiGroup = v1StoreProductsApiGroup.MapGroup("/my");

        v1.My.AddToStore.Endpoint.MapEndpoint(v1MyStoreProductsApiGroup);
        v1.My.Update.Endpoint.MapEndpoint(v1MyStoreProductsApiGroup);
        v1.My.Get.Endpoint.MapEndpoint(v1MyStoreProductsApiGroup);

        // v2StoresApiGroup = versionedApiGroup
        //     .MapGroup("/stores")
        //     .WithTags("Stores")
        //     .MapToApiVersion(2)

        // v2.xxx.Endpoint.MapEndpoint(v2StoresApiGroup)
    }
}
