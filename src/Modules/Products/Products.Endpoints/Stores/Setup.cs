using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Products.Endpoints.Stores;

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
        v1.History.Endpoint.MapEndpoint(v1StoresApiGroup);
        v1.AddProduct.Endpoint.MapEndpoint(v1StoresApiGroup);
        v1.RemoveProduct.Endpoint.MapEndpoint(v1StoresApiGroup);
        v1.My.Create.Endpoint.MapEndpoint(v1StoresApiGroup);
        v1.My.Update.Endpoint.MapEndpoint(v1StoresApiGroup);
        v1.My.Get.Endpoint.MapEndpoint(v1StoresApiGroup);
        v1.My.History.Endpoint.MapEndpoint(v1StoresApiGroup);
        v1.My.AddProduct.Endpoint.MapEndpoint(v1StoresApiGroup);
        v1.My.RemoveProduct.Endpoint.MapEndpoint(v1StoresApiGroup);

        // v2StoresApiGroup = versionedApiGroup
        //     .MapGroup("/stores")
        //     .WithTags("Stores")
        //     .MapToApiVersion(2)

        // v2.xxx.Endpoint.MapEndpoint(v2StoresApiGroup)
    }
}
