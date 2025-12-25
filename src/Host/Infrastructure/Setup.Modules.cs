using BackgroundJobs;
using Host.Middlewares;
using IAM.Endpoints;
using IAM.Infrastructure;
using IAM.Infrastructure.RateLimiting;
using Notifications.Infrastructure;
using Outbox;
using Products.Endpoints;
using Products.Infrastructure;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace Host.Infrastructure;

internal static partial class Setup
{
    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddBackgroundJobsModule(configuration)
            .AddOutboxModule()
            .AddNotificationsModule()
            .AddIAMModule(configuration)
            .AddProductsModule()
            .AddCustomRateLimiting(
                configuration,
                Policies.Get(),
                Products.Infrastructure.RateLimiting.Policies.Get())
            .AddFluentValidationAutoValidation();
    }

    public static IApplicationBuilder UseModules(this WebApplication app)
    {
        app.UseOutboxModule(); // Should be the first one to use
        app.UseNotificationsModule();
        app.UseIAMModule();
        app.UseProductsModule();
        app.UseBackgroundJobsModule();

        return app;
    }

    public static IApplicationBuilder MapModuleEndpoints(this WebApplication app)
    {
        var versionNeutralApiGroup = app
            .MapGroup("/")
            .AddFluentValidationAutoValidation()
            .RequireAuthorization();

        var apiVersionSet = app.GetApiVersionSet();

        // I did not extend this group from "/" root group because I got errors doing so (probably because of versioning)
        // So decided to create it from scratch.
        var versionedApiGroup = app
            .MapGroup("/v{version:apiVersion}")
            .AddFluentValidationAutoValidation()
            .WithApiVersionSet(apiVersionSet)
            .RequireAuthorization();

        app.MapIAMModuleEndpoints(versionNeutralApiGroup);
        app.MapProductsModuleEndpoints(versionedApiGroup);

        return app;
    }
}
