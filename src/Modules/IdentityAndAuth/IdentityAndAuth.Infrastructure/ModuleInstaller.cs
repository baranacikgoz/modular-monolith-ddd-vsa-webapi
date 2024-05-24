using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityAndAuth.Infrastructure.Persistence;
using IdentityAndAuth.Infrastructure.Identity;
using IdentityAndAuth.Infrastructure.Auth;
using IdentityAndAuth.Infrastructure.Tokens;
using IdentityAndAuth.Application.Identity;
using IdentityAndAuth.Application.Tokens;
using IdentityAndAuth.Application.Captcha;
using IdentityAndAuth.Infrastructure.Captcha;

namespace IdentityAndAuth.Infrastructure;

public static class ModuleInstaller
{
    public static IServiceCollection AddIdentityAndAuthModule(this IServiceCollection services, IConfiguration configuration)
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

    public static WebApplication UseIdentityAndAuthModule(
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
