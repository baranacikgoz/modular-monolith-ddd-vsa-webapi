using Host.Middlewares;
using Common.Localization;
using IdentityAndAuth.ModuleSetup;
using Common.Core.Interfaces;
using Common.Caching;
using Common.EventBus;
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
            .AddRequestResponseLoggingMiddleware()
            .AddResxLocalization()
            .AddGlobalExceptionHandlingMiddleware()
            .AddCustomProblemDetailsFactory()
            .AddCaching()
            .AddEndpointsApiExplorer()
            .AddMonitoringAndTracing(configuration)
            .AddCustomCors();

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        => app
            .UseRequestResponseLoggingMiddleware()
            .UseResxLocalization()
            .UseRateLimiter()
            .UseCors()
            .UseGlobalExceptionHandlingMiddleware()
            .UseAuth()
            .UseMonitoringAndTracing(configuration);

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

    private static IServiceCollection AddCustomProblemDetailsFactory(this IServiceCollection services)
        => services.AddSingleton<IProblemDetailsFactory, ProblemDetailsFactory>();
}
