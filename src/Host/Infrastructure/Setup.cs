using System.Reflection;
using Common.Application.JsonConverters;
using Common.Application.Options;
using Common.Infrastructure.Caching;
using Common.Infrastructure.EventBus;
using Common.Infrastructure.Localization;
using Common.Infrastructure.Persistence;
using Common.InterModuleRequests;
using FluentValidation;
using Host.Middlewares;
using IAM.Infrastructure;
using Microsoft.AspNetCore.Http.Json;
using IAssemblyReference = Common.Infrastructure.IAssemblyReference;

namespace Host.Infrastructure;

internal static partial class Setup
{
    private static readonly Assembly[] _moduleAssemblies =
    [
        typeof(IAssemblyReference).Assembly,

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

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment env)
    {
        return services
            .Configure<JsonOptions>(x =>
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
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        return app
            .UseRequestResponseLoggingMiddleware()
            .UseCommonResxLocalization()
            .UseRateLimiter()
            .UseCors()
            .UseGlobalExceptionHandlingMiddleware()
            .UseAuth(x => x.UseMiddleware<EnrichLogsWithUserInfoMiddleware>())
            .UseObservability();
    }

    private static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        return services
            .AddCors(options =>
            {
                // Change this in production.
                options.AddDefaultPolicy(builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
    }

    private static IServiceCollection AddCustomizedProblemDetails(this IServiceCollection services,
        IWebHostEnvironment env)
    {
        return services.AddProblemDetails(opt =>
        {
            opt.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Instance =
                    $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path.Value}";

                context.ProblemDetails.Extensions.TryAdd("traceId", context.HttpContext.TraceIdentifier);
                context.ProblemDetails.Extensions.TryAdd("environment", env.EnvironmentName);
            };
        });
    }

    private static IServiceCollection AddCommonDependencies(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddCommonCaching(config)
            .AddCommonEventBus(config, _moduleAssemblies)
            .AddCommonInterModuleRequests()
            .AddCommonResxLocalization()
            .AddCommonOptions(config)
            .AddCommonPersistence();
    }

    private static IServiceCollection AddEnrichLogsWithUserInfoMiddlware(this IServiceCollection services)
    {
        return services.AddScoped<EnrichLogsWithUserInfoMiddleware>();
    }
}
