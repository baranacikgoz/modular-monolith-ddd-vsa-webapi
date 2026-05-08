using Common.Application.Options;
using Microsoft.Extensions.Options;

namespace Host.Middlewares;

internal sealed class SecurityHeadersMiddleware(RequestDelegate next, IOptions<SecurityHeadersOptions> options)
{
    public Task InvokeAsync(HttpContext context)
    {
        foreach (var (name, value) in options.Value.Headers)
        {
            context.Response.Headers[name] = value;
        }

        return next(context);
    }
}
