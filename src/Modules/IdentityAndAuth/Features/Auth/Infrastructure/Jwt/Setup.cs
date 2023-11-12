using Common.Core.Contracts;
using Common.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

                    var stringLocalizer = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<JwtOptions>>();
                    var problemDetails = new CustomProblemDetails
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Title = stringLocalizer["Giriş yapmanız gerekmektedir."],
                        Type = "Unauthorized",
                        Instance = context.Request.Path,
                        RequestId = context.HttpContext.TraceIdentifier,
                        Errors = Enumerable.Empty<string>()
                    };

                    context.Response.StatusCode = problemDetails.Status;
                    context.Response.ContentType = "application/json";

                    return context.Response.WriteAsJsonAsync(problemDetails);
                },

                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];

                    // If the request is for our hub...
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs", StringComparison.Ordinal))
                    {
                        // Read the token out of the query string
                        context.Token = accessToken;
                    }

                    return Task.CompletedTask;
                },

                OnAuthenticationFailed = context =>
                {
                    var stringLocalizer = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<JwtOptions>>();
                    var problemDetails = new CustomProblemDetails
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Title = stringLocalizer["Giriş yapmanız gerekmektedir."],
                        Type = "Unauthorized",
                        Instance = context.Request.Path,
                        RequestId = context.HttpContext.TraceIdentifier,
                        Errors = Enumerable.Empty<string>()
                    };

                    return problemDetails.ExecuteAsync(context.HttpContext);
                },

                OnForbidden = context =>
                {
                    var stringLocalizer = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<JwtOptions>>();
                    var problemDetails = new CustomProblemDetails
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Title = stringLocalizer["Bu işlemi yapmaya yetkiniz bulunmamaktadır."],
                        Type = "Forbidden",
                        Instance = context.Request.Path,
                        RequestId = context.HttpContext.TraceIdentifier,
                        Errors = Enumerable.Empty<string>()
                    };

                    return problemDetails.ExecuteAsync(context.HttpContext);
                },

            };
        });

        return services;
    }
}
