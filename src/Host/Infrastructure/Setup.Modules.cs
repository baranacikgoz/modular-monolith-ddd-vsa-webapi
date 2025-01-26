using Host.Middlewares;
using IAM.Infrastructure;
using Notifications.Infrastructure;
using Outbox;
using BackgroundJobs;
using Products.Infrastructure;
using IAM.Endpoints;
using Products.Endpoints;

namespace Host.Infrastructure;

internal static partial class Setup
{
    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddBackgroundJobsModule()
            .AddOutboxModule()
            .AddNotificationsModule()
            .AddIAMModule(configuration)
            .AddProductsModule()
            .AddCustomRateLimiting(
                configuration,
                IAM.Infrastructure.RateLimiting.Policies.Get(),
                Products.Infrastructure.RateLimiting.Policies.Get());

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
                                    .RequireAuthorization()
                                    .WithOpenApi();

        var apiVersionSet = app.GetApiVersionSet();

        // I did not extend this group from "/" root group because I got errors doing so (probably because of versioning)
        // So decided to create it from scratch.
        var versionedApiGroup = app
                                .MapGroup("/v{version:apiVersion}")
                                .WithApiVersionSet(apiVersionSet)
                                .RequireAuthorization()
                                .WithOpenApi();

        app.MapIAMModuleEndpoints(versionNeutralApiGroup);
        app.MapProductsModuleEndpoints(versionedApiGroup);

        return app;
    }
}
