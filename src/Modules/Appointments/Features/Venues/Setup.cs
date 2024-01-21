using Appointments.Features.Venues.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Endpoint = Appointments.Features.Venues.UseCases.v1.Create.Endpoint;

namespace Appointments.Features.Venues;

internal static class Setup
{
    public static IServiceCollection AddVenuesFeatures(this IServiceCollection services)
        => services
            .AddVenuesInfrastructure();

    public static void MapVenuesEndpoints(this RouteGroupBuilder versionedApiGroup)
    {
        var v1VenuesApiGroup = versionedApiGroup
            .MapGroup("/venues")
            .WithTags("Venues")
            .MapToApiVersion(1);

        Venues.UseCases.v1.Create.Endpoint.MapEndpoint(v1VenuesApiGroup);

        // var v2VenuesApiGroup = versionedApiGroup
        //     .MapGroup("/venues")
        //     .WithTags("Venues")
        //     .MapToApiVersion(2)

        // Venues.UseCases.v2.xxx.Endpoint.MapEndpoint(v2VenuesApiGroup)
    }
}
