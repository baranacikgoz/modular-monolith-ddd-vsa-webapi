using Appointments.Features.Appointments.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Appointments.Features.Appointments;

internal static class Setup
{
    public static IServiceCollection AddAppointmentsFeatures(this IServiceCollection services)
        => services
            .AddAppointmentsInfrastructure();

    public static RouteGroupBuilder MapAppointmentsEndpoints(this RouteGroupBuilder rootGroup)
    {
        var appointmentsApiGroup = rootGroup
            .MapGroup("/appointments")
            .WithTags("Appointments");

        Create.Endpoint.MapEndpoint(appointmentsApiGroup);

        return rootGroup;
    }
}
