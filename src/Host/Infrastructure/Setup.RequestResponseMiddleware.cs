using Common.Infrastructure.Options;
using Host.Middlewares;
using Microsoft.Extensions.Options;

namespace Host.Infrastructure;

public static partial class Setup
{
    private static IServiceCollection AddRequestResponseLoggingMiddleware(this IServiceCollection services)
        => services.AddSingleton<RequestResponseLoggingMiddleware>();
    private static IApplicationBuilder UseRequestResponseLoggingMiddleware(this IApplicationBuilder app)
    {
        var backgroundJobsDashboardPath = app.ApplicationServices.GetRequiredService<IOptions<BackgroundJobsOptions>>().Value.DashboardPath;

        app
         .UseWhen(
             context => context.Request.Path != "/metrics" && context.Request.Path != backgroundJobsDashboardPath,
             appBuilder => appBuilder.UseMiddleware<RequestResponseLoggingMiddleware>());

        return app;
    }
}
