using Microsoft.AspNetCore.Http;

namespace IdentityAndAuth.Features.Auth.Extensions;

public static class HttpContextExtensions
{
    public static string? GetIpAddress(this HttpContext httpContext)
        => httpContext?
                .Request
                .Headers["X-Forwarded-For"]
                .FirstOrDefault()
                ??
            httpContext?
                .Request
                .Headers["X-Real-IP"]
                .FirstOrDefault()
                ??
            httpContext?
                .Connection
                .RemoteIpAddress?
                .ToString();
}
