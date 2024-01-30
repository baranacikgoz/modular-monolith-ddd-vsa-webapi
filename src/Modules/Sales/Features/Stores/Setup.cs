using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Sales.Features.Stores.Infrastructure;

namespace Sales.Features.Stores;

internal static class Setup
{
    public static IServiceCollection AddStoresFeature(this IServiceCollection services)
        => services
            .AddStoresInfrastructure();

    public static void MapStoresEndpoints(this RouteGroupBuilder versionedApiGroup)
    {
        var v1ProductsApiGroup = versionedApiGroup
            .MapGroup("/stores")
            .WithTags("Stores")
            .MapToApiVersion(1);

        Stores.UseCases.v1.Create.Endpoint.MapEndpoint(v1ProductsApiGroup);

        // v2StoresApiGroup = versionedApiGroup
        //     .MapGroup("/stores")
        //     .WithTags("Stores")
        //     .MapToApiVersion(2)

        // Stores.UseCases.v2.xxx.Endpoint.MapEndpoint(v2StoresApiGroup)
    }
}
