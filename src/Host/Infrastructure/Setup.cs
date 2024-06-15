using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Host.Validation;
using Common.Infrastructure.JsonConverters;
using Common.Infrastructure.Localization;
using Common.Infrastructure.Caching;
using System.Reflection;
using Common.Infrastructure.EventBus;
using Common.InterModuleRequests;
using Common.Infrastructure.Options;
using Common.Infrastructure.Persistence;
using IAM.Infrastructure;
using Host.Middlewares;

namespace Host.Infrastructure;

public static partial class Setup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
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
            .AddMetricsAndTracing(configuration)
            .AddCustomCors()
            .AddCommonDependencies(env, configuration)
            .AddFluentValidationAndAutoValidation()
            .AddEnrichLogsWithUserInfoMiddlware();

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        => app
            .UseRequestResponseLoggingMiddleware()
            .UseCommonResxLocalization()
            .UseRateLimiter()
            .UseCors()
            .UseGlobalExceptionHandlingMiddleware()
            .UseAuth(betweenAuthenticationAndAuthorization: app => app.UseMiddleware<EnrichLogsWithUserInfoMiddleware>());
    // .UsePrometheusScraping()

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

    private static IServiceCollection AddCommonDependencies(this IServiceCollection services, IWebHostEnvironment env, IConfiguration config)
        => services
            .AddCommonCaching()
            .AddCommonEventBus(env, config, _moduleAssemblies)
            .AddCommonInterModuleRequests()
            .AddCommonResxLocalization()
            .AddCommonOptions(config)
            .AddCommonPersistence();

    private static IServiceCollection AddFluentValidationAndAutoValidation(this IServiceCollection services)
        => services
            .AddValidatorsFromAssemblies(_moduleAssemblies)
            .AddFluentValidationAutoValidation(cfg => cfg.OverrideDefaultResultFactoryWith<CustomFluentValidationResultFactory>());

    private static readonly Assembly[] _moduleAssemblies =
        [
            typeof(IAM.Domain.IAssemblyReference).Assembly,
            typeof(IAM.Application.IAssemblyReference).Assembly,
            typeof(IAM.Infrastructure.IAssemblyReference).Assembly,

            typeof(Inventory.Domain.IAssemblyReference).Assembly,
            typeof(Inventory.Application.IAssemblyReference).Assembly,
            typeof(Inventory.Infrastructure.IAssemblyReference).Assembly,

            typeof(Notifications.Domain.IAssemblyReference).Assembly,
            typeof(Notifications.Application.IAssemblyReference).Assembly,
            typeof(Notifications.Infrastructure.IAssemblyReference).Assembly
        ];

    private static IServiceCollection AddEnrichLogsWithUserInfoMiddlware(this IServiceCollection services)
        => services.AddScoped<EnrichLogsWithUserInfoMiddleware>();
}
