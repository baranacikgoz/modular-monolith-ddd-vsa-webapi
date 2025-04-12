using FluentValidation;
using Common.Application.JsonConverters;
using Common.Infrastructure.Localization;
using Common.Infrastructure.Caching;
using System.Reflection;
using Common.Infrastructure.EventBus;
using Common.InterModuleRequests;
using Common.Infrastructure.Persistence;
using IAM.Infrastructure;
using Host.Middlewares;
using Common.Infrastructure.CQS;
using Common.Application.Options;

namespace Host.Infrastructure;

internal static partial class Setup
{
    private static readonly Assembly[] _moduleAssemblies =
        [
            typeof(Common.Infrastructure.IAssemblyReference).Assembly,

            typeof(IAM.Domain.IAssemblyReference).Assembly,
            typeof(IAM.Application.IAssemblyReference).Assembly,
            typeof(IAM.Infrastructure.IAssemblyReference).Assembly,
            typeof(IAM.Endpoints.IAssemblyReference).Assembly,

            typeof(Products.Domain.IAssemblyReference).Assembly,
            typeof(Products.Application.IAssemblyReference).Assembly,
            typeof(Products.Infrastructure.IAssemblyReference).Assembly,
            typeof(Products.Endpoints.IAssemblyReference).Assembly,

            typeof(Notifications.Domain.IAssemblyReference).Assembly,
            typeof(Notifications.Application.IAssemblyReference).Assembly,
            typeof(Notifications.Infrastructure.IAssemblyReference).Assembly
        ];

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
            .AddObservability(
                configuration,
                env,
                [])
            .AddCustomCors()
            .AddValidatorsFromAssemblies(_moduleAssemblies)
            .AddCommonDependencies(configuration)
            .AddEnrichLogsWithUserInfoMiddlware();

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        => app
            .UseRequestResponseLoggingMiddleware()
            .UseCommonResxLocalization()
            .UseRateLimiter()
            .UseCors()
            .UseGlobalExceptionHandlingMiddleware()
            .UseAuth(betweenAuthenticationAndAuthorization: x => x.UseMiddleware<EnrichLogsWithUserInfoMiddleware>())
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

                context.ProblemDetails.Extensions.TryAdd("traceId", context.HttpContext.TraceIdentifier);
                context.ProblemDetails.Extensions.TryAdd("environment", env.EnvironmentName);
            };
        });

    private static IServiceCollection AddCommonDependencies(this IServiceCollection services, IConfiguration config)
        => services
            .AddCommonCaching(config)
            .AddCommonEventBus(config, _moduleAssemblies)
            .AddCommonInterModuleRequests()
            .AddCommonResxLocalization()
            .AddCommonOptions(config)
            .AddCommonPersistence()
            .AddCommonCommandsQueriesHandlers(_moduleAssemblies);

    private static IServiceCollection AddEnrichLogsWithUserInfoMiddlware(this IServiceCollection services)
        => services.AddScoped<EnrichLogsWithUserInfoMiddleware>();
}
