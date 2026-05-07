using Common.Application.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Host.Middlewares;

internal sealed class SecurityHeadersMiddleware(RequestDelegate next, IOptions<SecurityHeadersOptions> options)
{
    private readonly SecurityHeadersOptions _options = options.Value;

    public Task InvokeAsync(HttpContext context)
    {
        foreach (var (name, value) in _options.Headers)
            context.Response.Headers[name] = value;

        return next(context);
    }
}
