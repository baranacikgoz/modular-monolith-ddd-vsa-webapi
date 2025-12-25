using Common.Application.Options;
using Host.Middlewares;
using Microsoft.Extensions.Options;

namespace Host.Infrastructure;

internal static partial class Setup
{
    private static IServiceCollection AddRequestResponseLoggingMiddleware(this IServiceCollection services)
    {
        return services.AddSingleton<RequestResponseLoggingMiddleware>();
    }

    private static IApplicationBuilder UseRequestResponseLoggingMiddleware(this IApplicationBuilder app)
    {
        var backgroundJobsDashboardPath = app.ApplicationServices.GetRequiredService<IOptions<BackgroundJobsOptions>>()
            .Value.DashboardPath;

        app
            .UseWhen(
                context => !IsMetricsEndpoint(context) &&
                           !IsBackgroundJobsDashboardEndpoint(context, backgroundJobsDashboardPath),
                appBuilder => appBuilder.UseMiddleware<RequestResponseLoggingMiddleware>());

        return app;
    }

    private static bool IsMetricsEndpoint(HttpContext context)
    {
        return context.Request.Path == "/metrics";
    }

    private static bool IsBackgroundJobsDashboardEndpoint(HttpContext context, string backgroundJobsDashboardPath)
    {
        return context.Request.Path.StartsWithSegments(backgroundJobsDashboardPath, StringComparison.OrdinalIgnoreCase);
    }
}
