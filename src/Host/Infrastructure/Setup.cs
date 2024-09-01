using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Host.Validation;
using Common.Application.JsonConverters;
using Common.Infrastructure.Localization;
using Common.Infrastructure.Caching;
using System.Reflection;
using Common.Infrastructure.EventBus;
using Common.InterModuleRequests;
using Common.Infrastructure.Options;
using Common.Infrastructure.Persistence;
using Host.Middlewares;
using IAM.Infrastructure;
using Common.Infrastructure;

namespace Host.Infrastructure;

public static partial class Setup
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment env,
        IEnumerable<IModule> modules)
        => services
            .Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(x =>
            {
                x.SerializerOptions.Converters.Add(new StronglyTypedIdWriteOnlyJsonConverter());
            })
            .Configure<HostOptions>(x =>
            {
                x.ServicesStartConcurrently = true;
                x.ServicesStopConcurrently = true;
            })
            .AddVersioning()
            .AddHttpContextAccessor()
            .AddRequestResponseLoggingMiddleware()
            .AddGlobalExceptionHandlingMiddleware()
            .AddCustomizedProblemDetails(env)
            .AddEndpointsApiExplorer()
            .AddObservability(
                configuration,
                env,
                modules.SelectMany(m => m.EfCoreInstrumentationFilters()))
            .AddCustomCors()
            .AddCommonDependencies(env, configuration, modules)
            .AddFluentValidationAndAutoValidation(modules)
            .AddEnrichLogsWithUserInfoMiddlware();

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        => app
            .UseRequestResponseLoggingMiddleware()
            .UseCommonResxLocalization()
            .UseRateLimiter()
            .UseCors()
            .UseGlobalExceptionHandlingMiddleware()
            .UseAuth(betweenAuthenticationAndAuthorization: app => app.UseMiddleware<EnrichLogsWithUserInfoMiddleware>())
            .UseObservability();

    private static IServiceCollection AddCustomCors(this IServiceCollection services)
        => services
            .AddCors(options =>
            {
                // Change this in production.
                options.AddDefaultPolicy(builder =>
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

    private static IServiceCollection AddCustomizedProblemDetails(this IServiceCollection services, IWebHostEnvironment env)
        => services.AddProblemDetails(opt =>
        {
            opt.CustomizeProblemDetails = (context) =>
            {
                context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path.Value}";

                context.ProblemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
                context.ProblemDetails.Extensions.Add("environment", env.EnvironmentName);
                context.ProblemDetails.Extensions.Add("node", Environment.MachineName);
            };
        });

    private static IServiceCollection AddCommonDependencies(
        this IServiceCollection services,
        IWebHostEnvironment env,
        IConfiguration config,
        IEnumerable<IModule> modules)
        => services
            .AddCommonCaching()
            .AddCommonEventBus(env, config, modules.SelectMany(m => m.GetAssemblies()))
            .AddCommonInterModuleRequests()
            .AddCommonResxLocalization()
            .AddCommonOptions(config)
            .AddCommonPersistence();

    private static IServiceCollection AddFluentValidationAndAutoValidation(this IServiceCollection services, IEnumerable<IModule> modules)
        => services
            .AddValidatorsFromAssemblies(modules.SelectMany(m => m.GetAssemblies()))
            .AddFluentValidationAutoValidation(cfg => cfg.OverrideDefaultResultFactoryWith<CustomFluentValidationResultFactory>());

    private static IServiceCollection AddEnrichLogsWithUserInfoMiddlware(this IServiceCollection services)
        => services.AddScoped<EnrichLogsWithUserInfoMiddleware>();
}
