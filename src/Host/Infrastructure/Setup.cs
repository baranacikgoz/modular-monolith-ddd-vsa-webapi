using Common.Localization;
using IdentityAndAuth.ModuleSetup;
using Common.Core.Interfaces;
using Common.Caching;
using Common.Options;
using Common.Persistence;

namespace Host.Infrastructure;

public static partial class Setup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        => services
            .AddCommonOptions(configuration)
            .AddVersioning()
            .AddHttpContextAccessor()
            .AddRequestResponseLoggingMiddleware()
            .AddResxLocalization()
            .AddGlobalExceptionHandlingMiddleware()
            .AddCustomizedProblemDetails(env)
            .AddCaching()
            .AddEndpointsApiExplorer()
            .AddMetricsAndTracing(configuration)
            .AddCustomCors();

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        => app
            .UseRequestResponseLoggingMiddleware()
            .UseResxLocalization()
            .UseRateLimiter()
            .UseCors()
            .UseGlobalExceptionHandlingMiddleware()
            .UseAuth();
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
}
