using Common.Application.Auth;
using IAM.Application.Auth.Services;
using IAM.Infrastructure.Auth.Jwt;
using IAM.Infrastructure.Auth.Services;
using IAM.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IAM.Infrastructure.Auth;

internal static class Setup
{
    internal static IServiceCollection AddAuthInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
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

    private static IServiceCollection AddCurrentUser(this IServiceCollection services)
    {
        return services
            .AddScoped<ICurrentUser, CurrentUser>(sp =>
            {
                var httpContext = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
                var user = httpContext?.User;

                return new CurrentUser(user);
            });
    }
}
