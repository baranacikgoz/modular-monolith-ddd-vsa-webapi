using IdentityAndAuth.Features.Auth;
using IdentityAndAuth.Features.Tokens;
using IdentityAndAuth.Persistence.Seeding;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityAndAuth.Features.Captcha;
using IdentityAndAuth.Features.Identity;
using IdentityAndAuth.Persistence;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace IdentityAndAuth.ModuleSetup;

public static class ModuleInstaller
{
    public static IServiceCollection AddIdentityAndAuthModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<Seeder>();

        services
            .AddPersistence()
            .AddIdentityFeature()
            .AddAuthFeature(configuration)
            .AddTokensFeature();

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

        versionNeutralApiGroup.MapUsersEndpoints();
        versionNeutralApiGroup.MapTokensEndpoints();
        versionNeutralApiGroup.MapCaptchaEndpoints();

        return app;
    }
}
