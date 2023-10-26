using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Appointments.Features.Venues;

public static class Setup
{
    public static RouteGroupBuilder MapVenuesEndpoints(this RouteGroupBuilder rootGroup)
    {
        var venuesApiGroup = rootGroup
            .MapGroup("/venues")
            .WithTags("Venues");

        CreateVenue.MapEndpoint(venuesApiGroup);
        UpdateVenue.MapEndpoint(venuesApiGroup);

        return rootGroup;
    }
}
