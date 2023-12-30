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
        var rootGroup = app
                        .MapGroup("/")
                        .RequireAuthorization()
                        .AddFluentValidationAutoValidation();

        app.UseIdentityAndAuthModule(rootGroup);
        app.UseAppointmentsModule(rootGroup);

        return app;
    }
}
