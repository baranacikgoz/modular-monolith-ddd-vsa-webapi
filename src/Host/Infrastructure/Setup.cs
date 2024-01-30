using Host.Middlewares;
using Common.Localization;
using IdentityAndAuth.ModuleSetup;
using Common.Core.Interfaces;
using Common.Caching;
using Common.Eventbus;
using FluentValidation;
using System.Reflection;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Host.Validation;
using Host.Swagger;
using Common.Options;
using Microsoft.Extensions.Localization;

namespace Host.Infrastructure;

public static partial class Setup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddCommonOptions(configuration)
            .AddVersioning()
            .AddHttpContextAccessor()
            .AddSingleton<RequestResponseLoggingMiddleware>()
            .AddResxLocalization()
            .AddRateLimiting(
                configuration,
                IdentityAndAuth.ModuleSetup.RateLimiting.Policies.Get(),
                Sales.ModuleSetup.RateLimiting.Policies.Get())
            .AddExceptionHandler<GlobalExceptionHandlingMiddleware>()
            .AddErrorLocalizer(
                IdentityAndAuth.ModuleSetup.ErrorLocalization.ErrorsAndLocalizations.Get(),
                Sales.ModuleSetup.ErrorLocalization.ErrorsAndLocalizations.Get()
            )
            .AddSingleton<IProblemDetailsFactory, ProblemDetailsFactory>()
            .AddCaching()
            .AddEventBus(
                typeof(IdentityAndAuth.IAssemblyReference).Assembly,
                typeof(Sales.IAssemblyReference).Assembly,
                typeof(Notifications.IAssemblyReference).Assembly)
            .AddFluentValidation()
            .AddEndpointsApiExplorer()
            .AddMonitoringAndTracing(configuration)
            .AddCustomCors();

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        => app
            .UseRequestResponseLoggingMiddleware()
            .UseResxLocalization()
            .UseRateLimiter()
            .UseCors()
            .UseExceptionHandlingMiddleware()
            .UseAuth()
            .UseMonitoringAndTracing(configuration);

    private static IApplicationBuilder UseRequestResponseLoggingMiddleware(this IApplicationBuilder app)
        => app
            .UseWhen(
                context => context.Request.Path != "/metrics",
                appBuilder => appBuilder.UseMiddleware<RequestResponseLoggingMiddleware>());

    private static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder app)
        => app.UseExceptionHandler(options => { });

    private static IServiceCollection AddErrorLocalizer(
        this IServiceCollection services,
        params IEnumerable<KeyValuePair<string, Func<IStringLocalizer, string>>>[] errorLocalizationsPerModule)
        => services
            .AddSingleton<IErrorLocalizer, AggregatedErrorLocalizer>(_ =>
            {
                return new AggregatedErrorLocalizer(errorLocalizationsPerModule.SelectMany(x => x));
            });

    private static IServiceCollection AddFluentValidation(this IServiceCollection services)
        => services
            .AddValidatorsFromAssemblies(
                new List<Assembly>()
                {
                    typeof(IdentityAndAuth.IAssemblyReference).Assembly,
                    typeof(Sales.IAssemblyReference).Assembly,
                    typeof(Notifications.IAssemblyReference).Assembly
                }
            )
            .AddFluentValidationAutoValidation(cfg => cfg.OverrideDefaultResultFactoryWith<CustomFluentValidationResultFactory>());

    private static IServiceCollection AddCustomCors(this IServiceCollection services)
        => services
            .AddCors(options =>
            {
                // Change this if production.
                options.AddDefaultPolicy(builder =>
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

}
