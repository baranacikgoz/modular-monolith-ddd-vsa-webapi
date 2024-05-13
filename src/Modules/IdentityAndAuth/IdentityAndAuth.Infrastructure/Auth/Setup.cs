using Common.Application.Auth;
using Common.Infrastructure.Extensions;
using IdentityAndAuth.Application.Auth.Services;
using IdentityAndAuth.Infrastructure.Auth.Jwt;
using IdentityAndAuth.Infrastructure.Auth.Services;
using IdentityAndAuth.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityAndAuth.Infrastructure.Auth;

internal static class Setup
{
    internal static IServiceCollection AddAuthInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
            .AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>()
            .AddJwtAuthentication(configuration)
            .AddAuthorization()
            .AddCurrentUser()
            .AddTransient<IRoleService, RoleService>();

        return services;
    }

    private static IServiceCollection AddCurrentUser(this IServiceCollection services) =>
        services
            .AddScoped<ICurrentUser, CurrentUser>(sp =>
            {
                var httpContext = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
                var user = httpContext?.User;
                var ipAddress = httpContext?.GetIpAddress();

                return new CurrentUser(user, ipAddress);
            });
}
