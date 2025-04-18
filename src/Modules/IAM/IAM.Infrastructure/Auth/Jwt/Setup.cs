using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Common.Application.Extensions;
using Common.Application.Localization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Common.Application.Options;

namespace IAM.Infrastructure.Auth.Jwt;

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
            options.TokenValidationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Secret)),

                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,

                ValidateLifetime = true,

                ValidAudience = jwtOptions.Audience,
                ValidateAudience = true,

                RoleClaimType = ClaimTypes.Role,

                ClockSkew = TimeSpan.Zero
            };

            options.Events = new JwtBearerEvents
            {
                OnChallenge = async context =>
                {
                    context.HandleResponse();
                    if (context.Response.HasStarted)
                    {
                        return;
                    }

                    var localizer = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<ResxLocalizer>>();
                    var problemDetailsService = context.HttpContext.RequestServices.GetRequiredService<IProblemDetailsService>();
                    var problemDetails = new ProblemDetails()
                    {
                        Status = (int)HttpStatusCode.Unauthorized,
                        Title = localizer[nameof(HttpStatusCode.Unauthorized)]
                    };

                    problemDetails.AddErrorKey(nameof(HttpStatusCode.Unauthorized));

                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await problemDetailsService.WriteAsync(new ProblemDetailsContext()
                    {
                        HttpContext = context.HttpContext,
                        ProblemDetails = problemDetails,
                    });
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

                OnAuthenticationFailed = async context =>
                {
                    if (context.HttpContext.GetEndpoint()?.Metadata?.GetMetadata<IAllowAnonymous>() is not null)
                    {
                        return;
                    }

                    var localizer = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<ResxLocalizer>>();
                    var problemDetailsService = context.HttpContext.RequestServices.GetRequiredService<IProblemDetailsService>();

                    var problemDetails = new ProblemDetails()
                    {
                        Status = (int)HttpStatusCode.Unauthorized,
                        Title = localizer[nameof(HttpStatusCode.Unauthorized)]
                    };

                    problemDetails.AddErrorKey(nameof(HttpStatusCode.Unauthorized));

                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await problemDetailsService.WriteAsync(new ProblemDetailsContext()
                    {
                        HttpContext = context.HttpContext,
                        ProblemDetails = problemDetails
                    });
                },

                OnForbidden = async context =>
                {
                    var localizer = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<ResxLocalizer>>();
                    var problemDetailsService = context.HttpContext.RequestServices.GetRequiredService<IProblemDetailsService>();

                    var problemDetails = new ProblemDetails()
                    {
                        Status = (int)HttpStatusCode.Forbidden,
                        Title = localizer[nameof(HttpStatusCode.Forbidden)]
                    };

                    problemDetails.AddErrorKey(nameof(HttpStatusCode.Forbidden));

                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    await problemDetailsService.WriteAsync(new ProblemDetailsContext()
                    {
                        HttpContext = context.HttpContext,
                        ProblemDetails = problemDetails
                    });
                },
            };
        });

        return services;
    }
}
