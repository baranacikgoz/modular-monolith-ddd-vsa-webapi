using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IAM.Application.Identity;
using IAM.Application.Captcha;
using IAM.Application.Tokens;
using IAM.Infrastructure.Identity;
using IAM.Infrastructure.Captcha;
using IAM.Infrastructure.Tokens;
using IAM.Infrastructure.Persistence;
using IAM.Infrastructure.Auth;

namespace IAM.Infrastructure;

public static class ModuleInstaller
{
    public static IServiceCollection AddIAMModule(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddPersistence()
            .AddIdentityInfrastructure()
            .AddAuthInfrastructure(configuration)
            .AddCaptchaInfrastructure()
            .AddTokensInfrastructure();

        return services;
    }

    public static IApplicationBuilder UseAuth(
        this IApplicationBuilder app,
        Action<IApplicationBuilder>? betweenAuthenticationAndAuthorization = null)
    {
        app.UseAuthentication();

        if (betweenAuthenticationAndAuthorization is not null)
        {
            betweenAuthenticationAndAuthorization(app);
        }

        app.UseAuthorization();

        return app;
    }

    public static WebApplication UseIAMModule(
        this WebApplication app,
        RouteGroupBuilder versionNeutralApiGroup)
    {
        app.UsePersistence();

        versionNeutralApiGroup.MapIdentityEndpoints();
        versionNeutralApiGroup.MapTokensEndpoints();
        versionNeutralApiGroup.MapCaptchaEndpoints();

        return app;
    }
}
