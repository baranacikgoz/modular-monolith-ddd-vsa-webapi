using System.Globalization;
using System.Security.Claims;
using Common.Application.Auth;
using Common.Application.Caching;
using Common.Application.Options;
using Common.Domain.StronglyTypedIds;
using IAM.Application.Keycloak;
using IAM.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Infrastructure.Auth;

/// <summary>
///     Asks Keycloak whether the caller's token is granted <c>resource#scope</c> and caches the answer per
///     (token jti, permission) for at most the token's remaining lifetime. Entries are tagged with the session and
///     the user so <see cref="IKeycloakAdminClient" /> can purge them when either is revoked. A transport failure
///     propagates, so an unreachable Keycloak fails closed (500) instead of silently denying or allowing.
/// </summary>
internal sealed class KeycloakPermissionAuthorizationHandler(
    IKeycloakPermissionClient permissionClient,
    IFusionCache cache,
    IHttpContextAccessor httpContextAccessor,
    IOptions<KeycloakOptions> keycloakOptionsProvider,
    TimeProvider timeProvider
) : AuthorizationHandler<KeycloakPermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        KeycloakPermissionRequirement requirement)
    {
        if (context.User.Identity?.IsAuthenticated != true)
        {
            return;
        }

        // Endpoint authorization passes the HttpContext as the resource; Hangfire's dashboard filter and
        // IAuthorizationService callers pass none, so fall back to the ambient request.
        var httpContext = context.Resource as HttpContext ?? httpContextAccessor.HttpContext;
        if (httpContext is null)
        {
            return;
        }

        var accessToken = await AccessTokenReader.ReadAsync(httpContext);
        if (accessToken is null)
        {
            return;
        }

        var permission = requirement.Permission.PolicyName();
        var cancellationToken = httpContext.RequestAborted;

        var granted = await DecideAsync(context.User, accessToken, permission, cancellationToken);

        if (granted)
        {
            context.Succeed(requirement);
        }
    }

    private async Task<bool> DecideAsync(ClaimsPrincipal user, string accessToken, string permission,
        CancellationToken cancellationToken)
    {
        var duration = RemainingCacheDuration(user);
        var jti = user.FindFirstValue(JwtClaimNames.Jti);

        if (jti is null || duration <= TimeSpan.Zero)
        {
            // No stable key or an already-expired token: ask Keycloak, never cache.
            var decision = await permissionClient.DecideAsync(accessToken, permission, cancellationToken);
            IamTelemetry.RecordAuthorizationDecision(decision, fromCache: false);
            return decision;
        }

        var fromCache = true;
        var granted = await cache.GetOrSetAsync(
            CacheKeys.For.AuthorizationDecision(jti, permission),
            async ct =>
            {
                fromCache = false;
                return await permissionClient.DecideAsync(accessToken, permission, ct);
            },
            // IsFailSafeEnabled = false: a Keycloak outage must surface as an error, not as a stale decision
            // served for up to FailSafeMaxDuration.
            new FusionCacheEntryOptions { Duration = duration, IsFailSafeEnabled = false },
            RevocationTags(user),
            cancellationToken);

        IamTelemetry.RecordAuthorizationDecision(granted, fromCache);
        return granted;
    }

    /// <summary>Session tag is absent for service accounts (no <c>sid</c>); they are only purged per user.</summary>
    private static List<string> RevocationTags(ClaimsPrincipal user)
    {
        var tags = new List<string>(2);

        if (DefaultIdType.TryParse(user.FindFirstValue(JwtClaimNames.Subject), out var subject))
        {
            tags.Add(CacheKeys.For.AuthorizationDecisionUserTag(new ApplicationUserId(subject)));
        }

        if (user.FindFirstValue(JwtClaimNames.SessionId) is { Length: > 0 } sessionId)
        {
            tags.Add(CacheKeys.For.AuthorizationDecisionSessionTag(sessionId));
        }

        return tags;
    }

    private TimeSpan RemainingCacheDuration(ClaimsPrincipal user)
    {
        var maxDuration = TimeSpan.FromSeconds(keycloakOptionsProvider.Value.DecisionCacheMaxDurationSeconds);

        var expiration = user.FindFirstValue(JwtClaimNames.Expiration);
        if (!long.TryParse(expiration, NumberStyles.Integer, CultureInfo.InvariantCulture, out var expiresAtUnix))
        {
            return TimeSpan.Zero;
        }

        var remaining = DateTimeOffset.FromUnixTimeSeconds(expiresAtUnix) - timeProvider.GetUtcNow();
        return remaining < maxDuration ? remaining : maxDuration;
    }
}

/// <summary>Reads the bearer token JwtBearer validated for this request (<c>SaveToken = true</c>).</summary>
public static class AccessTokenReader
{
    public static async Task<string?> ReadAsync(HttpContext httpContext)
    {
        var token = await httpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
        if (!string.IsNullOrEmpty(token))
        {
            return token;
        }

        // Fallback for principals authenticated by a different handler that still carries a bearer header.
        var authorization = httpContext.Request.Headers.Authorization.ToString();
        const string prefix = "Bearer ";
        return authorization.StartsWith(prefix, StringComparison.OrdinalIgnoreCase) && authorization.Length > prefix.Length
            ? authorization[prefix.Length..].Trim()
            : null;
    }
}
