using IdentityAndAuth.Features.Users;
using IdentityAndAuth.Auth.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Common.Core.Auth;
using IdentityAndAuth.Features.Users.Services;
using Microsoft.AspNetCore.Authorization;
namespace IdentityAndAuth.Auth;

internal static class Setup
{
    internal static IServiceCollection AddCustomAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
            .AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>()
            .AddJwtAuthentication(configuration)
            .AddAuthorization()
            .AddCurrentUser();

        return services;
    }

    private static IServiceCollection AddCurrentUser(this IServiceCollection services) =>
        services
            .AddScoped<ICurrentUser, CurrentUser>(sp =>
            {
                var httpContext = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
                var user = httpContext?.User;
                var ipAddress = GetIpAddress(httpContext);

                return new CurrentUser(user, ipAddress);
            });
    private static string? GetIpAddress(HttpContext? httpContext)
    {
        var ipAddress = httpContext?.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (string.IsNullOrEmpty(ipAddress))
        {
            ipAddress = httpContext?.Request.Headers["X-Real-IP"].FirstOrDefault();
        }
        if (string.IsNullOrEmpty(ipAddress))
        {
            ipAddress = httpContext?.Connection.RemoteIpAddress?.ToString();
        }

        return ipAddress;
    }
}
