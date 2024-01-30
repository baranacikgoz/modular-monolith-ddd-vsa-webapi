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
using Microsoft.Extensions.Localization;

namespace Host.Infrastructure;

public static partial class Setup
{
    private static IServiceCollection AddRequestResponseLoggingMiddleware(this IServiceCollection services)
        => services.AddSingleton<RequestResponseLoggingMiddleware>();
    private static IApplicationBuilder UseRequestResponseLoggingMiddleware(this IApplicationBuilder app)
        => app
            .UseWhen(
                context => context.Request.Path != "/metrics",
                appBuilder => appBuilder.UseMiddleware<RequestResponseLoggingMiddleware>());
}
