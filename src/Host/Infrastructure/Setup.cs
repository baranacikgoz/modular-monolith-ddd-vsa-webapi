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

namespace Host.Infrastructure;

public static partial class Setup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddCommonOptions(configuration)
            .AddHttpContextAccessor()
            .AddSingleton<RequestResponseLoggingMiddleware>()
            .AddCustomLocalization("Resources")
            .AddRateLimiting(configuration)
            .AddSingleton<ExceptionHandlingMiddleware>()
            .AddErrorLocalizer()
            .AddSingleton<IProblemDetailsFactory, ProblemDetailsFactory>()
            .AddCaching()
            .AddEventBus()
            .AddFluentValidation()
            .AddEndpointsApiExplorer()
            .AddCustomSwagger()
            .AddMonitoringAndTracing(configuration);

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IWebHostEnvironment env)
        => app
            .UseRequestResponseLoggingMiddleware()
            .UseCustomLocalization()
            .UseRateLimiter()
            .UseExceptionHandlingMiddleware()
            .UseAuth()
            .UseCustomSwagger(env);

    private static IApplicationBuilder UseRequestResponseLoggingMiddleware(this IApplicationBuilder app)
        => app
            .UseWhen(
                context => context.Request.Path != "/metrics",
                appBuilder => appBuilder.UseMiddleware<RequestResponseLoggingMiddleware>());

    private static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder app)
        => app
            .UseMiddleware<ExceptionHandlingMiddleware>();

    private static IServiceCollection AddErrorLocalizer(this IServiceCollection services)
        => services
            .AddSingleton<IErrorLocalizer, AggregatedErrorLocalizer>(_ =>
            {
                return new AggregatedErrorLocalizer(
                    IdentityAndAuth.ModuleSetup.ErrorLocalization.ErrorsAndLocalizations.Get(),
                    Appointments.ModuleSetup.ErrorLocalization.ErrorsAndLocalizations.Get()
                    );
            });

    private static IServiceCollection AddEventBus(this IServiceCollection services)
        => services
            .AddEventBus(
                typeof(Appointments.IAssemblyReference).Assembly,
                typeof(IdentityAndAuth.IAssemblyReference).Assembly,
                typeof(Notifications.IAssemblyReference).Assembly);

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

}
