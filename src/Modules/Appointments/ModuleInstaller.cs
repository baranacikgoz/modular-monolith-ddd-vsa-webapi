using Appointments.Common.Services;
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
    {
        services.AddKeyedTransient<IErrorTranslator, LocalizedErrorTranslator>(ModuleConstants.ModuleName);
        services.AddKeyedTransient<IResultTranslator, ResultTranslator>(
            ModuleConstants.ModuleName,
            (sp, _) => new ResultTranslator(
                sp.GetRequiredKeyedService<IErrorTranslator>(ModuleConstants.ModuleName),
                sp.GetRequiredService<IHttpContextAccessor>()
                ));

        return services;
    }

    public static WebApplication UseAppointmentsModule(this WebApplication app, RouteGroupBuilder rootGroup)
    {
        rootGroup
            .MapVenuesEndpoints();

        return app;
    }
}
