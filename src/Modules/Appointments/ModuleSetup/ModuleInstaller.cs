using Appointments.Features.Appointments;
using Appointments.Features.Venues;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Appointments.ModuleSetup;

public static class ModuleInstaller
{
    public static IServiceCollection AddAppointmentsModule(this IServiceCollection services)
        => services
        .AddAppointmentsFeatures()
        .AddVenuesFeatures();

    public static WebApplication UseAppointmentsModule(this WebApplication app, RouteGroupBuilder rootGroup)
    {
        rootGroup
            .MapAppointmentsEndpoints()
            .MapVenuesEndpoints();

        return app;
    }
}
