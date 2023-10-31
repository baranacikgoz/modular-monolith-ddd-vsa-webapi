using IdentityAndAuth.Features.Tokens;
using IdentityAndAuth.Features.Users;
using IdentityAndAuth.Persistence.Seeding;
using IdentityAndAuth.Auth;
using IdentityAndAuth.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityAndAuth.Features.Captcha;
using IdentityAndAuth.Persistence;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace IdentityAndAuth;

public static class ModuleInstaller
{

    public static IServiceCollection InstallIdentityAndAuthModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<Seeder>();

        services
            .AddCustomIdentity()
            .AddCustomAuth(configuration);

        services
            .AddUsersFeatures()
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

    public static WebApplication UseIdentityAndAuthModule(this WebApplication app, RouteGroupBuilder rootGroup)
    {
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<IdentityContext>();
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<IdentityContext>>();
                logger.LogError(ex, "An error occurred while migrating the database.");
                throw;
            }

            var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
            try
            {
                seeder.SeedDb().Wait();
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Seeder>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        rootGroup
            .MapUsersEndpoints()
            .MapTokensEndpoints()
            .MapCaptchaEndpoints();

        return app;
    }
}
