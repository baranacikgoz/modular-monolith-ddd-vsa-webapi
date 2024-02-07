using Host.Middlewares;

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
