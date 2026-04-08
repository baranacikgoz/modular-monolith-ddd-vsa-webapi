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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Json;

namespace Host.Infrastructure;

internal static partial class Setup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment env)
    {
        var moduleAssemblies = services.GetActiveModuleAssemblies();

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
                env)
            .AddCustomCors(configuration)
            .AddValidatorsFromAssemblies(moduleAssemblies)
            .AddCommonDependencies(configuration, moduleAssemblies)
            .AddCustomHealthChecks(configuration)
            .AddEnrichLogsWithUserInfoMiddleware();
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app
            .UseRequestResponseLoggingMiddleware()
            .UseCommonResxLocalization()
            .UseRateLimiter()
            .UseCors()
            .UseGlobalExceptionHandlingMiddleware();

        var hasAuth = app.ApplicationServices.GetService<IAuthenticationSchemeProvider>() != null;
        if (hasAuth)
        {
            app.UseAuthentication()
                .UseMiddleware<EnrichLogsWithUserInfoMiddleware>()
                .UseAuthorization();
        }

        app.UseObservability();

        return app;
    }

    private static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration configuration)
    {
        var corsOptions = configuration
                              .GetSection(nameof(CorsOptions))
                              .Get<CorsOptions>()
                          ?? throw new InvalidOperationException($"Missing configuration: {nameof(CorsOptions)}.");

        return services
            .AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    if (corsOptions.AllowedOrigins.Count == 0)
                    {
                        builder.SetIsOriginAllowed(_ => false);
                    }
                    else if (corsOptions.AllowedOrigins is ["*"])
                    {
                        builder.AllowAnyOrigin();
                    }
                    else
                    {
                        builder.WithOrigins([.. corsOptions.AllowedOrigins]);
                    }

                    if (corsOptions.AllowedMethods is ["*"])
                    {
                        builder.AllowAnyMethod();
                    }
                    else
                    {
                        builder.WithMethods([.. corsOptions.AllowedMethods]);
                    }

                    if (corsOptions.AllowedHeaders is ["*"])
                    {
                        builder.AllowAnyHeader();
                    }
                    else
                    {
                        builder.WithHeaders([.. corsOptions.AllowedHeaders]);
                    }

                    if (corsOptions.AllowCredentials)
                    {
                        builder.AllowCredentials();
                    }
                    else
                    {
                        builder.DisallowCredentials();
                    }

                    builder.SetPreflightMaxAge(TimeSpan.FromSeconds(corsOptions.MaxAgeInSeconds));
                });
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

    private static IServiceCollection AddEnrichLogsWithUserInfoMiddleware(this IServiceCollection services)
    {
        return services.AddScoped<EnrichLogsWithUserInfoMiddleware>();
    }
}
