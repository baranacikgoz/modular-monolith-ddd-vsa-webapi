using Host.Middlewares;

namespace Host.Infrastructure;

public static partial class Setup
{
    private static IServiceCollection AddGlobalExceptionHandlingMiddleware(this IServiceCollection services)
        => services.AddExceptionHandler<GlobalExceptionHandlingMiddleware>();
    private static IApplicationBuilder UseGlobalExceptionHandlingMiddleware(this IApplicationBuilder app)
        => app.UseExceptionHandler(options => { });
}
