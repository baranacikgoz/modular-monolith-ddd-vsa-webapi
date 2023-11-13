using Common.Core.Auth;
using Common.Core.Extensions;
using IdentityAndAuth.Features.Auth.Extensions;
using IdentityAndAuth.Features.Auth.Infrastructure.Jwt;
using IdentityAndAuth.Features.Identity.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityAndAuth.Features.Auth.Infrastructure;

internal static class Setup
{
    internal static IServiceCollection AddAuthInfrastructure(this IServiceCollection services, IConfiguration configuration)
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
                var ipAddress = httpContext?.GetIpAddress();

                return new CurrentUser(user, ipAddress);
            });
}
