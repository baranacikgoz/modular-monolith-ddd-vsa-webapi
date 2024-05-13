using Microsoft.AspNetCore.Http;

namespace Common.Infrastructure.Extensions;

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
