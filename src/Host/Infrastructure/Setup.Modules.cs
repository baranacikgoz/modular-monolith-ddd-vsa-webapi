using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using IdentityAndAuth.ModuleSetup;
using Appointments.ModuleSetup;

namespace Host.Infrastructure;

public static partial class Setup
{
    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddIdentityAndAuthModule(configuration)
            .AddAppointmentsModule();
    public static IApplicationBuilder UseModules(this WebApplication app)
    {
        var versionNeutralApiGroup = app
                                    .MapGroup("/")
                                    .RequireAuthorization()
                                    .AddFluentValidationAutoValidation()
                                    .WithOpenApi();

        var apiVersionSet = app.GetApiVersionSet();

        var versionedApiGroup = app
                                .MapGroup("/v{version:apiVersion}")
                                .WithApiVersionSet(apiVersionSet)
                                .RequireAuthorization()
                                .AddFluentValidationAutoValidation()
                                .WithOpenApi();

        app.UseIdentityAndAuthModule(versionNeutralApiGroup);
        app.UseAppointmentsModule(versionedApiGroup);

        return app;
    }
}
