using Common.Application.Auth;
using Serilog.Context;

namespace Host.Middlewares;

public class EnrichLogsWithUserInfoMiddleware(ICurrentUser currentUser) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var userId = string.IsNullOrEmpty(currentUser.IdAsString) ? "Anonymous" : currentUser.IdAsString;

        using (LogContext.PushProperty("User", userId))
        {
            await next(context);
        }
    }
}
