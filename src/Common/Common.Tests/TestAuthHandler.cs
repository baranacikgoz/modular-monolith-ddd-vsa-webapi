using System.Security.Claims;
using System.Text.Encodings.Web;
using Common.Application.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Common.Tests;

public class TestAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    public const string AuthenticationScheme = "TestScheme";
    public static readonly Guid DefaultUserId = Guid.NewGuid();

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var nameIdentifier = DefaultUserId.ToString();
        if (Request.Headers.TryGetValue("X-Test-User-Id", out var overrideId))
        {
            nameIdentifier = overrideId.ToString();
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, nameIdentifier),
            new(ClaimTypes.Name, "TestUser"),
            // Add any essential custom permissions required by all endpoints implicitly.
            // Tests can override this by injecting different headers or using specific identities if needed.
            new("Permission", CustomActions.Read + CustomResources.ApplicationUsers),
            new("Permission", CustomActions.Create + CustomResources.ApplicationUsers),
            new("Permission", CustomActions.Update + CustomResources.ApplicationUsers),
            new("Permission", CustomActions.Delete + CustomResources.ApplicationUsers),
            new("Permission", CustomActions.ReadMy + CustomResources.ApplicationUsers),
            new("Permission", CustomActions.UpdateMy + CustomResources.ApplicationUsers),
        };

        var identity = new ClaimsIdentity(claims, AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, AuthenticationScheme);

        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }
}
