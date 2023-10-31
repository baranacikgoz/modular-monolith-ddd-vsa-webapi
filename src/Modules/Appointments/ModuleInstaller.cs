using Appointments.Features.Appointments;
using Appointments.Features.Venues;
using Common.Caching;
using Common.Core.Contracts;
using Common.Core.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Appointments;

public static class ModuleInstaller
{
    public static IServiceCollection InstallAppointmentsModule(this IServiceCollection services)
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
