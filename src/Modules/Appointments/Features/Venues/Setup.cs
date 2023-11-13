using Appointments.Features.Venues.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Endpoint = Appointments.Features.Venues.UseCases.Create.Endpoint;

namespace Appointments.Features.Venues;

internal static class Setup
{
    public static IServiceCollection AddVenuesFeatures(this IServiceCollection services)
        => services
            .AddVenuesInfrastructure();

    public static RouteGroupBuilder MapVenuesEndpoints(this RouteGroupBuilder rootGroup)
    {
        var venuesApiGroup = rootGroup
            .MapGroup("/venues")
            .WithTags("Venues");

        Endpoint.MapEndpoint(venuesApiGroup);

        return rootGroup;
    }
}
