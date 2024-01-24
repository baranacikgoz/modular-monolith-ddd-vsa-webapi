using Common.Core.Contracts;
using Common.Core.Interfaces;
using Common.Localization;
using Common.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace IdentityAndAuth.Features.Auth.Infrastructure.Jwt;

internal static class Setup
{
    internal static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
        ArgumentNullException.ThrowIfNull(jwtOptions);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = CustomTokenValidationParameters.Get(jwtOptions);

            options.Events = new JwtBearerEvents
            {
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    if (context.Response.HasStarted)
                    {
                        return Task.CompletedTask;
                    }

                    var localizer = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<ResxLocalizer>>();
                    var problemDetailsFactory = context.HttpContext.RequestServices.GetRequiredService<IProblemDetailsFactory>();
                    var problemDetails = problemDetailsFactory.Create(
                        status: StatusCodes.Status401Unauthorized,
                        title: localizer["Giriş yapmanız gerekmektedir."],
                        type: "Unauthorized",
                        instance: context.Request.Path,
                        requestId: context.HttpContext.TraceIdentifier,
                        errors: Enumerable.Empty<string>()
                    );

                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";

                    return problemDetails.ExecuteAsync(context.HttpContext);
                },

                OnMessageReceived = context =>
                {
                    // If the request is for our hub...
                    var path = context.HttpContext.Request.Path;
                    if (path.StartsWithSegments("/hubs", StringComparison.Ordinal))
                    {
                        var accessToken = context.Request.Query["access_token"];
                        // Read the token out of the query string
                        context.Token = accessToken;
                    }

                    return Task.CompletedTask;
                },

                OnAuthenticationFailed = context =>
                {
                    if (context.HttpContext.GetEndpoint()?.Metadata?.GetMetadata<IAllowAnonymous>() is not null)
                    {
                        return Task.CompletedTask;
                    }

                    var localizer = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<ResxLocalizer>>();
                    var problemDetailsFactory = context.HttpContext.RequestServices.GetRequiredService<IProblemDetailsFactory>();
                    var localizedMessage = localizer["Giriş yapmanız gerekmektedir."];
                    var problemDetails = problemDetailsFactory.Create(
                        status: StatusCodes.Status401Unauthorized,
                        title: localizedMessage,
                        type: "Unauthorized",
                        instance: context.Request.Path,
                        requestId: context.HttpContext.TraceIdentifier,
                        errors: Enumerable.Empty<string>()
                    );

                    return problemDetails.ExecuteAsync(context.HttpContext);
                },

                OnForbidden = context =>
                {
                    var localizer = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<ResxLocalizer>>();
                    var problemDetailsFactory = context.HttpContext.RequestServices.GetRequiredService<IProblemDetailsFactory>();

                    var problemDetails = problemDetailsFactory.Create(
                        status: StatusCodes.Status403Forbidden,
                        title: localizer["Bu işlemi yapmaya yetkiniz bulunmamaktadır."],
                        type: "Forbidden",
                        instance: context.Request.Path,
                        requestId: context.HttpContext.TraceIdentifier,
                        errors: Enumerable.Empty<string>()
                    );

                    return problemDetails.ExecuteAsync(context.HttpContext);
                },

            };
        });

        return services;
    }
}
