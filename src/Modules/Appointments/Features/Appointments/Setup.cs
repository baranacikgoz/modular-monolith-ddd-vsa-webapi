using Appointments.Features.Appointments.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Endpoint = Appointments.Features.Appointments.UseCases.Book.Endpoint;

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

        Endpoint.MapEndpoint(appointmentsApiGroup);

        return rootGroup;
    }
}
