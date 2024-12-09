using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Host.Middlewares;
using IAM.Infrastructure;
using Notifications.Infrastructure;
using Outbox;
using BackgroundJobs;
using Products.Infrastructure;

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

        app.UseOutboxModule();
        app.UseNotificationsModule();
        app.UseIAMModule(versionNeutralApiGroup);
        app.UseProductsModule(versionedApiGroup);
        app.UseBackgroundJobsModule();

        return app;
    }

    private static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
        => services.AddRateLimiting(
                configuration,
                IAM.Infrastructure.RateLimiting.Policies.Get(),
                Products.Infrastructure.RateLimiting.Policies.Get());
}
