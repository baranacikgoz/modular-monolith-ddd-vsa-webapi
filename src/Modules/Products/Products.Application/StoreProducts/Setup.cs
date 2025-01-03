using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Products.Application.StoreProducts;
public static class Setup
{
    public static void MapStoreProductsEndpoints(this RouteGroupBuilder versionedApiGroup)
    {
        var v1StoreProductsApiGroup = versionedApiGroup
            .MapGroup("/storeproducts")
            .WithTags("Store Products")
            .MapToApiVersion(1);

        v1.AddToStore.Endpoint.MapEndpoint(v1StoreProductsApiGroup);
        v1.Get.Endpoint.MapEndpoint(v1StoreProductsApiGroup);
        v1.Update.Endpoint.MapEndpoint(v1StoreProductsApiGroup);
        v1.Search.Endpoint.MapEndpoint(v1StoreProductsApiGroup);
        v1.Delete.Endpoint.MapEndpoint(v1StoreProductsApiGroup);

        var v1MyStoreProductsApiGroup = v1StoreProductsApiGroup.MapGroup("/my");
        v1.My.AddToMyStore.Endpoint.MapEndpoint(v1MyStoreProductsApiGroup);
        v1.My.Update.Endpoint.MapEndpoint(v1MyStoreProductsApiGroup);
        v1.My.Search.Endpoint.MapEndpoint(v1MyStoreProductsApiGroup);
        v1.My.Get.Endpoint.MapEndpoint(v1MyStoreProductsApiGroup);
        v1.My.Delete.Endpoint.MapEndpoint(v1MyStoreProductsApiGroup);

        // v2StoresApiGroup = versionedApiGroup
        //     .MapGroup("/stores")
        //     .WithTags("Stores")
        //     .MapToApiVersion(2)

        // v2.xxx.Endpoint.MapEndpoint(v2StoresApiGroup)
    }
}
