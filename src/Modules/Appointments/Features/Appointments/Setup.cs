using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Appointments.Features.Appointments;

public static class Setup
{
    public static IServiceCollection AddAppointmentsFeatures(this IServiceCollection services)
    {
        return services;
    }

    public static RouteGroupBuilder MapAppointmentsEndpoints(this RouteGroupBuilder rootGroup)
    {
        var appointmentsApiGroup = rootGroup
            .MapGroup("/appointments")
            .WithTags("Appointments");

        Create.Endpoint.MapEndpoint(appointmentsApiGroup);

        return rootGroup;
    }
}
