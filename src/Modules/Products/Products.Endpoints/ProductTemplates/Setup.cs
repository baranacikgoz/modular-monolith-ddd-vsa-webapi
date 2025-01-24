using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Products.Endpoints.ProductTemplates;

public static class Setup
{
    public static void MapProductTemplatesEndpoints(this RouteGroupBuilder versionedApiGroup)
    {
        var v1ProductTemplatesApiGroup = versionedApiGroup
            .MapGroup("/product-templates")
            .WithTags("ProductTemplates")
            .MapToApiVersion(1);

        v1.Create.Endpoint.MapEndpoint(v1ProductTemplatesApiGroup);
        v1.Get.Endpoint.MapEndpoint(v1ProductTemplatesApiGroup);
        v1.Search.Endpoint.MapEndpoint(v1ProductTemplatesApiGroup);
        v1.Activate.Endpoint.MapEndpoint(v1ProductTemplatesApiGroup);
        v1.Deactivate.Endpoint.MapEndpoint(v1ProductTemplatesApiGroup);
    }
}
