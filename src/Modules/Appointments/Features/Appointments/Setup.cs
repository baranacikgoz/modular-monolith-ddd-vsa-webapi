using Appointments.Features.Appointments.Infrastructure;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Endpoint = Appointments.Features.Appointments.UseCases.v1.Book.Endpoint;

namespace Appointments.Features.Appointments;

internal static class Setup
{
    public static IServiceCollection AddAppointmentsFeatures(this IServiceCollection services)
        => services
            .AddAppointmentsInfrastructure();

    public static void MapAppointmentsEndpoints(this RouteGroupBuilder versionedApiGroup)
    {
        var v1AppointmentsApiGroup = versionedApiGroup
            .MapGroup("/appointments")
            .WithTags("Appointments")
            .MapToApiVersion(1);

        Appointments.UseCases.v1.Book.Endpoint.MapEndpoint(v1AppointmentsApiGroup);

        // v2AppointmentsApiGroup = versionedApiGroup
        //     .MapGroup("/appointments")
        //     .WithTags("Appointments")
        //     .MapToApiVersion(2)

        // Appointments.UseCases.v2.xxx.Endpoint.MapEndpoint(v2AppointmentsApiGroup)
    }
}
