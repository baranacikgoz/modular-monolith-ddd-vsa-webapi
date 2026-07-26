using IAM.Application.Auth.Services;
using IAM.Infrastructure.Auth.ApiKey;
using IAM.Infrastructure.Auth.Jwt;
using IAM.Infrastructure.Auth.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = MultiAuthDefaults.Scheme;
                options.DefaultChallengeScheme = MultiAuthDefaults.Scheme;
            })
            // Routes each request to whichever scheme matches its credential — every existing
            // MustHavePermission(...) policy resolves against the default scheme, so this is the
            // one place that makes them accept both JWT and API-key callers with zero endpoint changes.
            .AddPolicyScheme(MultiAuthDefaults.Scheme, MultiAuthDefaults.Scheme, options =>
                options.ForwardDefaultSelector = context =>
                    context.Request.Headers.ContainsKey(ApiKeyDefaults.HeaderName)
                        ? ApiKeyDefaults.Scheme
                        : JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearerScheme(configuration)
            .AddApiKeyScheme();

        services
            .AddAuthorization()
            .AddTransient<IRoleService, RoleService>();

        return services;
    }
}
