using Appointments.Features.Appointments;
using Appointments.Features.Venues;
using Appointments.Persistence;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Appointments.ModuleSetup;

public static class ModuleInstaller
{
    public static IServiceCollection AddAppointmentsModule(this IServiceCollection services)
        => services
            .AddPersistence()
            .AddAppointmentsFeatures()
            .AddVenuesFeatures();

    public static WebApplication UseAppointmentsModule(
        this WebApplication app,
        RouteGroupBuilder versionedApiGroup)
    {
        app.UsePersistence();

        versionedApiGroup.MapAppointmentsEndpoints();
        versionedApiGroup.MapVenuesEndpoints();

        return app;
    }
}
