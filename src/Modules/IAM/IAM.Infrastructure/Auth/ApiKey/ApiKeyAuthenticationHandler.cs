using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Localization.Resources;
using Common.Application.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IAM.Infrastructure.Auth.ApiKey;

internal sealed partial class ApiKeyAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> schemeOptions,
    ILoggerFactory loggerFactory,
    UrlEncoder encoder,
    IOptionsMonitor<ApiKeysOptions> apiKeysOptions
) : AuthenticationHandler<AuthenticationSchemeOptions>(schemeOptions, loggerFactory, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(ApiKeyDefaults.HeaderName, out var provided) ||
            string.IsNullOrEmpty(provided))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        var rawKey = provided.ToString();
        var matchedEntry = apiKeysOptions.CurrentValue.Keys?
            .FirstOrDefault(entry => ApiKeyHasher.Matches(rawKey, entry.KeyHash));

        if (matchedEntry is null)
        {
            LogRejected(Logger);
            return Task.FromResult(AuthenticateResult.Fail("Invalid API key."));
        }

        Activity.Current?.SetTag("apikey.name", matchedEntry.Name);

        var claims = new List<Claim> { new(ClaimTypes.Name, matchedEntry.Name) };
        claims.AddRange(matchedEntry.Permissions.Select(p => new Claim(JwtClaimNames.Permission, p)));

        var identity = new ClaimsIdentity(claims, ApiKeyDefaults.Scheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, ApiKeyDefaults.Scheme);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }

    protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        if (Response.HasStarted)
        {
            return;
        }

        var localizer = Context.RequestServices.GetRequiredService<IResxLocalizer>();
        var problemDetailsService = Context.RequestServices.GetRequiredService<IProblemDetailsService>();

        var problemDetails = new ProblemDetails
        {
            Status = (int)HttpStatusCode.Unauthorized, Title = localizer.Unauthorized
        };
        problemDetails.AddErrorKey(nameof(HttpStatusCode.Unauthorized));

        Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        await problemDetailsService.WriteAsync(new ProblemDetailsContext
        {
            HttpContext = Context, ProblemDetails = problemDetails
        });
    }

    protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
    {
        var localizer = Context.RequestServices.GetRequiredService<IResxLocalizer>();
        var problemDetailsService = Context.RequestServices.GetRequiredService<IProblemDetailsService>();

        var problemDetails = new ProblemDetails
        {
            Status = (int)HttpStatusCode.Forbidden, Title = localizer.Forbidden
        };
        problemDetails.AddErrorKey(nameof(HttpStatusCode.Forbidden));

        Response.StatusCode = (int)HttpStatusCode.Forbidden;
        await problemDetailsService.WriteAsync(new ProblemDetailsContext
        {
            HttpContext = Context, ProblemDetails = problemDetails
        });
    }

    [LoggerMessage(Level = LogLevel.Warning, Message = "Rejected request with an unrecognized API key.")]
    private static partial void LogRejected(ILogger logger);
}
