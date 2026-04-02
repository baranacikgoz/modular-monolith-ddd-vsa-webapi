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

public static class Setup
{
    public static IServiceCollection AddAuthInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
            .AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>()
            .AddJwtAuthentication(configuration)
            .AddAuthorization()
            .AddTransient<IRoleService, RoleService>();

        return services;
    }

}
