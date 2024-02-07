using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using IdentityAndAuth.ModuleSetup;
using Sales.ModuleSetup;
using Host.Middlewares;
using Host.Validation;
using Common.EventBus;
using FluentValidation;

namespace Host.Infrastructure;

public static partial class Setup
{
    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        => services
            .AddIdentityAndAuthModule(configuration)
            .AddSalesModule()
            .AddRateLimiting(configuration)
            .AddFluentValidationAndAutoValidation()
            .AddEventBus(env);

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

    private static IServiceCollection AddFluentValidationAndAutoValidation(this IServiceCollection services)
        => services
            .AddValidatorsFromAssemblies(
                [
                    typeof(IdentityAndAuth.IAssemblyReference).Assembly,
                    typeof(Sales.IAssemblyReference).Assembly,
                    typeof(Notifications.IAssemblyReference).Assembly
                ])
            .AddFluentValidationAutoValidation(cfg => cfg.OverrideDefaultResultFactoryWith<CustomFluentValidationResultFactory>());

    private static IServiceCollection AddEventBus(this IServiceCollection services, IWebHostEnvironment env)
        => services
            .AddEventBus(
                env,
                typeof(IdentityAndAuth.IAssemblyReference).Assembly,
                typeof(Sales.IAssemblyReference).Assembly,
                typeof(Notifications.IAssemblyReference).Assembly);
}
