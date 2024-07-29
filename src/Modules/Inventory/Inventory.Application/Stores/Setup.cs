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

        v1.Create.Endpoint.MapEndpoint(v1StoresApiGroup);
        v1.Update.Endpoint.MapEndpoint(v1StoresApiGroup);
        v1.Get.Endpoint.MapEndpoint(v1StoresApiGroup);
        v1.Search.Endpoint.MapEndpoint(v1StoresApiGroup);
        v1.Delete.Endpoint.MapEndpoint(v1StoresApiGroup);

        var v1MyStoresApiGroup = v1StoresApiGroup.MapGroup("/my");
        v1.My.Create.Endpoint.MapEndpoint(v1MyStoresApiGroup);
        v1.My.Update.Endpoint.MapEndpoint(v1MyStoresApiGroup);
        v1.My.Get.Endpoint.MapEndpoint(v1MyStoresApiGroup);

        var v1MyStoresStoreProductsApiGroup = v1MyStoresApiGroup.MapGroup("/storeproducts");
        v1.My.StoreProducts.Add.Endpoint.MapEndpoint(v1MyStoresStoreProductsApiGroup);
        v1.My.StoreProducts.Get.Endpoint.MapEndpoint(v1MyStoresStoreProductsApiGroup);
        v1.My.StoreProducts.Update.Endpoint.MapEndpoint(v1MyStoresStoreProductsApiGroup);
        
        // v2StoresApiGroup = versionedApiGroup
        //     .MapGroup("/stores")
        //     .WithTags("Stores")
        //     .MapToApiVersion(2)

        // v2.xxx.Endpoint.MapEndpoint(v2StoresApiGroup)
    }
}
