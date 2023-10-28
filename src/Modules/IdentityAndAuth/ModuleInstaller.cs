﻿using Common.Caching;
using IdentityAndAuth.Features.Tokens;
using IdentityAndAuth.Features.Users;
using IdentityAndAuth.Persistence.Seeding;
using IdentityAndAuth.Auth;
using IdentityAndAuth.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Common.Core.Contracts;
using Common.Core.Implementations;
using Microsoft.AspNetCore.Http;
using IdentityAndAuth.Features.Common;
using IdentityAndAuth.Features.Captcha;

namespace IdentityAndAuth;

public static class ModuleInstaller
{

    public static IServiceCollection InstallIdentityAndAuthModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<Seeder>();

        services
            .AddCustomIdentity()
            .AddCustomAuth(configuration)
            .AddCommonFeatures();

        services
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
            var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
            seeder.SeedDb().Wait();
        }

        rootGroup
            .MapUsersEndpoints()
            .MapTokensEndpoints()
            .MapCaptchaEndpoints();

        return app;
    }
}
