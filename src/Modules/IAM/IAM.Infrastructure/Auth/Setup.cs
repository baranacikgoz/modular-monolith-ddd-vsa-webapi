using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IAM.Infrastructure.Auth;

public static class Setup
{
    public static IServiceCollection AddAuthInfrastructure(this IServiceCollection services)
    {
        services
            .AddSingleton<IConfigureOptions<JwtBearerOptions>, JwtBearerConfigureOptions>()
            .AddSingleton<IAuthorizationPolicyProvider, KeycloakPermissionPolicyProvider>()
            .AddScoped<IAuthorizationHandler, KeycloakPermissionAuthorizationHandler>()
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.AddAuthorization();

        return services;
    }
}
