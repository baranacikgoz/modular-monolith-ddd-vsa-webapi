using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Host.Middlewares;
using IAM.Infrastructure;
using Inventory.Infrastructure;
using Notifications.Infrastructure;

namespace Host.Infrastructure;

public static partial class Setup
{
    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        => services
            .AddIAMModule(configuration)
            .AddInventoryModule()
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

        app.UseIAMModule(versionNeutralApiGroup);
        app.UseInventoryModule(versionedApiGroup);

        return app;
    }

    private static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
        => services.AddRateLimiting(
                configuration,
                IAM.Infrastructure.RateLimiting.Policies.Get(),
                Inventory.Infrastructure.RateLimiting.Policies.Get()); 
}
