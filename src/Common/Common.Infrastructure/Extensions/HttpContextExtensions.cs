using Common.Application.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Extensions;

public static class HttpContextExtensions
{
    // Deliberately not reading X-Forwarded-For / X-Real-IP here: those are raw client-supplied headers.
    // UseForwardedHeaders() (see Host/Infrastructure/Setup.ForwardedHeaders.cs) already validates the
    // forwarding chain against ReverseProxyOptions.TrustedNetworks and rewrites RemoteIpAddress before
    // this ever runs, that's the only IP a rate limiter partition key can trust.
    public static string? GetIpAddress(this HttpContext httpContext) =>
        httpContext?.Connection.RemoteIpAddress?.ToString();

    // Rate limit partition key for user-scoped policies. Falls back to IP: UseRateLimiter() runs before
    // UseAuthorization(), so an unauthenticated caller can still reach a policy on an authenticated endpoint.
    public static string GetUserIdOrIpAddress(this HttpContext httpContext)
    {
        var userId = httpContext.RequestServices.GetService<ICurrentUser>()?.IdAsString;
        return string.IsNullOrEmpty(userId) ? httpContext.GetIpAddress() ?? "unknown" : userId;
    }
}
