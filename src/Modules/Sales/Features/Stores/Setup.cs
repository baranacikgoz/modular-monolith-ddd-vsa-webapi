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
        var v1StoresApiGroup = versionedApiGroup
            .MapGroup("/stores")
            .WithTags("Stores")
            .MapToApiVersion(1);

        var v1MyStoresApiGroup = v1StoresApiGroup.MapGroup("/my");
        UseCases.v1.My.Create.Endpoint.MapEndpoint(v1MyStoresApiGroup);

        // v2StoresApiGroup = versionedApiGroup
        //     .MapGroup("/stores")
        //     .WithTags("Stores")
        //     .MapToApiVersion(2)

        // Stores.UseCases.v2.xxx.Endpoint.MapEndpoint(v2StoresApiGroup)
    }
}
