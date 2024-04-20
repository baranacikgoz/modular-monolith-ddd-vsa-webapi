using Common.Localization;
using IdentityAndAuth.ModuleSetup;
using Common.Caching;
using Common.Options;
using Common.EventBus;
using Common.InterModuleRequests;
using Common.Persistence;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Host.Validation;
using Common.Core.JsonConverters;

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
            .AddDependenciesOfCommonProjects(env, configuration)
            .AddFluentValidationAndAutoValidation();

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        => app
            .UseRequestResponseLoggingMiddleware()
            .UseCommonResxLocalization()
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

    private static IServiceCollection AddDependenciesOfCommonProjects(this IServiceCollection services, IWebHostEnvironment env, IConfiguration config)
        => services
            .AddCommonCaching()
            .AddCommonEventBus(env, config)
            .AddCommonInterModuleRequests()
            .AddCommonResxLocalization()
            .AddCommonOptions(config)
            .AddCommonPersistence();

    private static IServiceCollection AddCommonEventBus(this IServiceCollection services, IWebHostEnvironment env, IConfiguration config)
        => services
            .AddCommonEventBus(
                env,
                config,
                typeof(IdentityAndAuth.IAssemblyReference).Assembly,
                typeof(Sales.IAssemblyReference).Assembly,
                typeof(Notifications.IAssemblyReference).Assembly);

    private static IServiceCollection AddFluentValidationAndAutoValidation(this IServiceCollection services)
        => services
            .AddValidatorsFromAssemblies(
                [
                    typeof(IdentityAndAuth.IAssemblyReference).Assembly,
                    typeof(Sales.IAssemblyReference).Assembly,
                    typeof(Notifications.IAssemblyReference).Assembly
                ])
            .AddFluentValidationAutoValidation(cfg => cfg.OverrideDefaultResultFactoryWith<CustomFluentValidationResultFactory>());
}
