using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
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
            .AddRateLimiting(configuration);

    public static IApplicationBuilder UseModules(this WebApplication app)
    {
        app.UseOutboxModule();
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

        app.MapIAMModuleEndpoints(versionNeutralApiGroup);
        app.MapProductsModuleEndpoints(versionedApiGroup);

        return app;
    }

    private static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
        => services.AddRateLimiting(
                configuration,
                IAM.Infrastructure.RateLimiting.Policies.Get(),
                Products.Infrastructure.RateLimiting.Policies.Get());
}
