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
            .AddCustomLocalization("Resources")
            .AddRateLimiting(
                configuration,
                IdentityAndAuth.ModuleSetup.RateLimiting.Policies.Get(),
                Appointments.ModuleSetup.RateLimiting.Policies.Get())
            .AddSingleton<ExceptionHandlingMiddleware>()
            .AddErrorLocalizer(
                IdentityAndAuth.ModuleSetup.ErrorLocalization.ErrorsAndLocalizations.Get(),
                Appointments.ModuleSetup.ErrorLocalization.ErrorsAndLocalizations.Get()
            )
            .AddSingleton<IProblemDetailsFactory, ProblemDetailsFactory>()
            .AddCaching()
            .AddEventBus(
                typeof(Appointments.IAssemblyReference).Assembly,
                typeof(IdentityAndAuth.IAssemblyReference).Assembly,
                typeof(Notifications.IAssemblyReference).Assembly)
            .AddFluentValidation()
            .AddEndpointsApiExplorer()
            .AddMonitoringAndTracing(configuration)
            .AddCustomCors();

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        => app
            .UseRequestResponseLoggingMiddleware()
            .UseCustomLocalization()
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
        => app
            .UseMiddleware<ExceptionHandlingMiddleware>();

    private static IServiceCollection AddErrorLocalizer(
        this IServiceCollection services,
        params IEnumerable<KeyValuePair<string, Func<IStringLocalizer<IErrorLocalizer>, string>>>[] errorLocalizationsPerModule)
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
                    typeof(Appointments.IAssemblyReference).Assembly,
                    typeof(IdentityAndAuth.IAssemblyReference).Assembly,
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
