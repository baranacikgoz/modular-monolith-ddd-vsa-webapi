using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using IdentityAndAuth.ModuleSetup;
using Sales.ModuleSetup;
using Host.Middlewares;
using Host.Validation;
using Common.EventBus;
using FluentValidation;
using Notifications.ModuleSetup;

namespace Host.Infrastructure;

public static partial class Setup
{
    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        => services
            .AddIdentityAndAuthModule(configuration)
            .AddSalesModule()
            .AddNotificationsModule()
            .AddRateLimiting(configuration);

    public static IApplicationBuilder UseModules(this WebApplication app)
    {
        var versionNeutralApiGroup = app
                                    .MapGroup("/")
                                    .RequireAuthorization()
                                    .AddFluentValidationAutoValidation()
                                    .WithOpenApi();

        var apiVersionSet = app.GetApiVersionSet();

        // I did not extend this group from "/" root group because I got errors doing so (probably because of versioning)
        // So decided to create it from scratch.
        var versionedApiGroup = app
                                .MapGroup("/v{version:apiVersion}")
                                .WithApiVersionSet(apiVersionSet)
                                .RequireAuthorization()
                                .AddFluentValidationAutoValidation()
                                .WithOpenApi();

        app.UseIdentityAndAuthModule(versionNeutralApiGroup);
        app.UseSalesModule(versionedApiGroup);

        return app;
    }

    private static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
        => services.AddRateLimiting(
                configuration,
                IdentityAndAuth.ModuleSetup.RateLimiting.Policies.Get(),
                Sales.ModuleSetup.RateLimiting.Policies.Get());   
}
