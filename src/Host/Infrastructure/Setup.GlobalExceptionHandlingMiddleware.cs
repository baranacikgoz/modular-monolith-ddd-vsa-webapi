using Host.Middlewares;

namespace Host.Infrastructure;

internal static partial class Setup
{
    private static IServiceCollection AddGlobalExceptionHandlingMiddleware(this IServiceCollection services)
    {
        return services.AddSingleton<GlobalExceptionHandlingMiddleware>();
    }

    private static IApplicationBuilder UseGlobalExceptionHandlingMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    }
}
