using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Common.Infrastructure;
using IAM.Infrastructure;

namespace Host.Infrastructure;

public static partial class Setup
{
    public static IServiceCollection RegisterModules(
        this IServiceCollection services,
        IEnumerable<IModule> modules,
        IConfiguration configuration,
        IWebHostEnvironment env)
    {
        foreach (var module in modules)
        {
            module.Register(services, configuration, env);
        }

        services.RegisterIAMModule(configuration);

        services.AddRateLimiting(
            configuration,
            modules.SelectMany(m => m.RateLimitingPolicies()));

        return services;
    }

    public static IApplicationBuilder UseModules(this WebApplication app, IEnumerable<IModule> modules)
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

        foreach (var module in modules.OrderBy(m => m.RegistrationPriority))
        {
            module.Use(app, versionedApiGroup);
        }

        app.UseIAMModule(versionNeutralApiGroup);

        return app;
    }
}
