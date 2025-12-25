using IAM.Infrastructure.Auth;
using IAM.Infrastructure.Captcha;
using IAM.Infrastructure.Identity;
using IAM.Infrastructure.Persistence;
using IAM.Infrastructure.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

    public static WebApplication UseIAMModule(this WebApplication app)
    {
        app.UsePersistence();

        return app;
    }
}
