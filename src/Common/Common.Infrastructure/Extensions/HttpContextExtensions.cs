using Microsoft.AspNetCore.Http;

namespace Common.Infrastructure.Extensions;

public static class HttpContextExtensions
{
    // Deliberately not reading X-Forwarded-For / X-Real-IP here: those are raw client-supplied headers.
    // UseForwardedHeaders() (see Host/Infrastructure/Setup.ForwardedHeaders.cs) already validates the
    // forwarding chain against ReverseProxyOptions.TrustedNetworks and rewrites RemoteIpAddress before
    // this ever runs — that's the only IP a rate limiter partition key can trust.
    public static string? GetIpAddress(this HttpContext httpContext) =>
        httpContext?.Connection.RemoteIpAddress?.ToString();
}
