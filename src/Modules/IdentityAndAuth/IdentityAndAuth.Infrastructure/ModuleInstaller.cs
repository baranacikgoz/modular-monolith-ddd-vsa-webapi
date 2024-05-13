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

namespace IdentityAndAuth.Infrastructure;

public static class ModuleInstaller
{
    public static IServiceCollection AddIdentityAndAuthModule(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddPersistence()
            .AddIdentityInfrastructure()
            .AddAuthInfrastructure(configuration)
            .AddTokensInfrastructure();

        return services;
    }

    public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
    {
        app
           .UseAuthentication()
           .UseAuthorization();

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
