using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Common.Application.Auth;
using Common.Application.Caching;
using Common.Application.Extensions;
using Common.Application.Localization.Resources;
using Common.Application.Options;
using Common.Infrastructure.Persistence.Extensions;
using IAM.Application.Persistence;
using IAM.Domain.Identity.Sessions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Infrastructure.Auth.Jwt;

internal static class Setup
{
    internal static AuthenticationBuilder AddJwtBearerScheme(this AuthenticationBuilder builder,
        IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
        ArgumentNullException.ThrowIfNull(jwtOptions);

        return builder
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                // Keep wire claim names verbatim ("role") instead of remapping to the long Microsoft URIs.
                options.MapInboundClaims = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateLifetime = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateAudience = true,
                    RoleClaimType = JwtClaimNames.Roles,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var sid = context.Principal?.FindFirstValue(JwtClaimNames.SessionId);
                        if (sid is null || !SessionId.TryParse(sid, out var sessionId))
                        {
                            context.Fail("Missing or malformed sid claim.");
                            return;
                        }

                        var cache = context.HttpContext.RequestServices.GetRequiredService<IFusionCache>();
                        var db = context.HttpContext.RequestServices.GetRequiredService<IIAMDbContext>();

                        // GetOrSetAsync's factory queries Postgres, the source of truth, so a Redis
                        // outage falls back to a DB read instead of reading a cache miss as "not
                        // revoked". IsFailSafeEnabled = false: on a factory failure this throws (routed
                        // to OnAuthenticationFailed -> 401) instead of serving a stale cached value for
                        // up to FailSafeMaxDuration.
                        var isValid = await cache.GetOrSetAsync(
                            CacheKeys.For.SessionValid(sessionId.Value),
                            async ct => await db.Sessions
                                .AsNoTracking()
                                .TagWith("OnTokenValidated", sessionId.Value)
                                .AnyAsync(s => s.Id == sessionId && s.RevokedAt == null, ct),
                            options: new FusionCacheEntryOptions
                            {
                                Duration = TimeSpan.FromSeconds(jwtOptions.SessionRevocationCacheDurationInSeconds),
                                IsFailSafeEnabled = false
                            },
                            token: context.HttpContext.RequestAborted);

                        if (!isValid)
                        {
                            context.Fail("Session has been revoked.");
                        }
                    },
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();
                        if (context.Response.HasStarted)
                        {
                            return;
                        }

                        var localizer = context.HttpContext.RequestServices
                            .GetRequiredService<IResxLocalizer>();
                        var problemDetailsService =
                            context.HttpContext.RequestServices.GetRequiredService<IProblemDetailsService>();
                        var problemDetails = new ProblemDetails
                        {
                            Status = (int)HttpStatusCode.Unauthorized, Title = localizer.Unauthorized
                        };

                        problemDetails.AddErrorKey(nameof(HttpStatusCode.Unauthorized));

                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        await problemDetailsService.WriteAsync(new ProblemDetailsContext
                        {
                            HttpContext = context.HttpContext, ProblemDetails = problemDetails
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

                        var localizer = context.HttpContext.RequestServices
                            .GetRequiredService<IResxLocalizer>();
                        var problemDetailsService =
                            context.HttpContext.RequestServices.GetRequiredService<IProblemDetailsService>();

                        var problemDetails = new ProblemDetails
                        {
                            Status = (int)HttpStatusCode.Unauthorized, Title = localizer.Unauthorized
                        };

                        problemDetails.AddErrorKey(nameof(HttpStatusCode.Unauthorized));

                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        await problemDetailsService.WriteAsync(new ProblemDetailsContext
                        {
                            HttpContext = context.HttpContext, ProblemDetails = problemDetails
                        });
                    },
                    OnForbidden = async context =>
                    {
                        var localizer = context.HttpContext.RequestServices
                            .GetRequiredService<IResxLocalizer>();
                        var problemDetailsService =
                            context.HttpContext.RequestServices.GetRequiredService<IProblemDetailsService>();

                        var problemDetails = new ProblemDetails
                        {
                            Status = (int)HttpStatusCode.Forbidden, Title = localizer.Forbidden
                        };

                        problemDetails.AddErrorKey(nameof(HttpStatusCode.Forbidden));

                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        await problemDetailsService.WriteAsync(new ProblemDetailsContext
                        {
                            HttpContext = context.HttpContext, ProblemDetails = problemDetails
                        });
                    }
                };
            });
    }
}
