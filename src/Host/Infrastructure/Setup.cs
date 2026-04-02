using System.Reflection;
using Common.Application.JsonConverters;
using Common.Application.Options;
using Common.Endpoints.Versioning;
using Common.Infrastructure.Auth;
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
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment env)
    {
        var moduleAssemblies = Setup.GetActiveModuleAssemblies(configuration);
        
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
            .AddValidatorsFromAssemblies(moduleAssemblies)
            .AddCommonDependencies(configuration, moduleAssemblies)
            .AddEnrichLogsWithUserInfoMiddlware();
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app
            .UseRequestResponseLoggingMiddleware()
            .UseCommonResxLocalization()
            .UseRateLimiter()
            .UseCors()
            .UseGlobalExceptionHandlingMiddleware();

        var hasAuth = app.ApplicationServices.GetService<Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider>() != null;
        if (hasAuth)
        {
            app.UseAuthentication()
               .UseMiddleware<EnrichLogsWithUserInfoMiddleware>()
               .UseAuthorization();
        }

        app.UseObservability();
        
        return app;
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

    private static IServiceCollection AddCommonDependencies(this IServiceCollection services, IConfiguration config,
        Assembly[] moduleAssemblies)
    {
        return services
            .AddCommonCaching(config)
            .AddCommonEventBus(config, moduleAssemblies)
            .AddCommonInterModuleRequests()
            .AddCommonResxLocalization()
            .AddCommonOptions(config)
            .AddCommonPersistence()
            .AddCommonAuth();
    }

    private static IServiceCollection AddEnrichLogsWithUserInfoMiddlware(this IServiceCollection services)
    {
        return services.AddScoped<EnrichLogsWithUserInfoMiddleware>();
    }
}
