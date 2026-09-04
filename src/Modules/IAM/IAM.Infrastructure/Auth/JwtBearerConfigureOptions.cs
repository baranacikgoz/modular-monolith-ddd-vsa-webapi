using System.Net;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Localization.Resources;
using Common.Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IAM.Infrastructure.Auth;

/// <summary>
///     Validates Keycloak access tokens through OIDC discovery of the realm. Configured as an options
///     configurator (not inline in AddJwtBearer) so <see cref="KeycloakOptions" /> is read at runtime, where
///     test overrides and environment variables are visible.
/// </summary>
internal sealed class JwtBearerConfigureOptions(IOptions<KeycloakOptions> keycloakOptionsProvider)
    : IConfigureNamedOptions<JwtBearerOptions>
{
    public void Configure(string? name, JwtBearerOptions options)
    {
        if (!string.Equals(name, JwtBearerDefaults.AuthenticationScheme, StringComparison.Ordinal))
        {
            return;
        }

        Configure(options);
    }

    public void Configure(JwtBearerOptions options)
    {
        var keycloak = keycloakOptionsProvider.Value;

        options.Authority = keycloak.Authority;
        options.Audience = keycloak.ResourceClientId;
        options.RequireHttpsMetadata = keycloak.RequireHttpsMetadata;

        // The permission handler re-sends the validated token to Keycloak's decision endpoint.
        options.SaveToken = true;

        // Keep wire claim names verbatim ("sub", "sid", "roles") instead of remapping to the long Microsoft URIs.
        options.MapInboundClaims = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = keycloak.Authority,
            ValidateAudience = true,
            ValidAudience = keycloak.ResourceClientId,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            NameClaimType = JwtClaimNames.PreferredUsername,
            RoleClaimType = JwtClaimNames.Roles,
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // SignalR cannot set headers on WebSocket upgrade: the hub client sends the token in the query string.
                var path = context.HttpContext.Request.Path;
                if (path.StartsWithSegments("/hubs", StringComparison.Ordinal))
                {
                    context.Token = context.Request.Query["access_token"];
                }

                return Task.CompletedTask;
            },
            OnChallenge = async context =>
            {
                context.HandleResponse();
                if (context.Response.HasStarted)
                {
                    return;
                }

                await WriteProblemAsync(context.HttpContext, HttpStatusCode.Unauthorized);
            },
            OnAuthenticationFailed = async context =>
            {
                if (context.HttpContext.GetEndpoint()?.Metadata.GetMetadata<IAllowAnonymous>() is not null)
                {
                    return;
                }

                await WriteProblemAsync(context.HttpContext, HttpStatusCode.Unauthorized);
            },
            OnForbidden = context => WriteProblemAsync(context.HttpContext, HttpStatusCode.Forbidden)
        };
    }

    private static async Task WriteProblemAsync(HttpContext httpContext, HttpStatusCode statusCode)
    {
        var localizer = httpContext.RequestServices.GetRequiredService<IResxLocalizer>();
        var problemDetailsService = httpContext.RequestServices.GetRequiredService<IProblemDetailsService>();

        var problemDetails = new ProblemDetails
        {
            Status = (int)statusCode,
            Title = statusCode == HttpStatusCode.Forbidden ? localizer.Forbidden : localizer.Unauthorized
        };
        problemDetails.AddErrorKey(statusCode.ToString());

        httpContext.Response.StatusCode = (int)statusCode;
        await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext, ProblemDetails = problemDetails
        });
    }
}
