using System.Security.Claims;
using System.Text.Encodings.Web;
using Common.Application.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Common.Tests;

/// <summary>
///     Stand-in for the Keycloak JwtBearer scheme: emits the same claim shape Keycloak access tokens carry
///     (<c>sub</c>, <c>jti</c>, <c>sid</c>, <c>roles</c>). Permission checks are short-circuited by
///     <see cref="AllowAllAuthorizationHandler" />, so no Keycloak instance is needed for slice tests.
/// </summary>
public class TestAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    public const string AuthenticationScheme = "TestScheme";
    public static readonly Guid DefaultUserId = Guid.NewGuid();
    public static readonly string DefaultJti = Guid.NewGuid().ToString();
    public static readonly string DefaultSessionId = "test-session-" + Guid.NewGuid().ToString("N")[..12];

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // If no Authorization header is present, consider the request unauthenticated.
        // This allows tests that call endpoints without setting Authorization to correctly receive 401.
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        var subject = DefaultUserId.ToString();
        if (Request.Headers.TryGetValue("X-Test-User-Id", out var overrideId))
        {
            subject = overrideId.ToString();
        }

        var jti = DefaultJti;
        if (Request.Headers.TryGetValue("X-Test-Jti", out var overrideJti))
        {
            jti = overrideJti.ToString();
        }

        var sessionId = DefaultSessionId;
        if (Request.Headers.TryGetValue("X-Test-Session-Id", out var overrideSessionId))
        {
            sessionId = overrideSessionId.ToString();
        }

        var claims = new List<Claim>
        {
            new(JwtClaimNames.Subject, subject),
            new(JwtClaimNames.Jti, jti),
            new(JwtClaimNames.SessionId, sessionId),
            new(JwtClaimNames.PreferredUsername, "test-user")
        };

        // Optional role claims, comma-separated header, e.g. "basic,system-admin".
        if (Request.Headers.TryGetValue("X-Test-Roles", out var roles))
        {
            claims.AddRange(roles
                .ToString()
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(role => new Claim(JwtClaimNames.Roles, role)));
        }

        var identity = new ClaimsIdentity(claims, AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, AuthenticationScheme);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }

    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Response.StatusCode = 401;
        return Task.CompletedTask;
    }
}
