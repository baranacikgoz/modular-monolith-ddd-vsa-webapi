using Serilog.Context;

namespace Host.Middlewares;

public class RequestResponseLoggingMiddleware(
    ILogger<RequestResponseLoggingMiddleware> logger
    ) : IMiddleware
{
    private const int ResponseTimeThresholdMs = 1000;
    private const string LogTemplate = "{Prefix} HTTP {Method} {Path} responded {StatusCode} in {Elapsed:0} ms";
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var start = DateTime.UtcNow;

        context.Response.OnCompleted(() =>
        {
            var elapsed = DateTime.UtcNow - start;
            var elapsedMs = elapsed.TotalMilliseconds;
            var isSlow = elapsedMs > ResponseTimeThresholdMs;

            using (LogContext.PushProperty("IsSlow", isSlow))
            using (LogContext.PushProperty("TraceId", context.TraceIdentifier))
            {
                LogResponse(context, elapsedMs, isSlow);
            }

            return Task.CompletedTask;
        });

        await next(context);
    }

    private void LogResponse(HttpContext context, double elapsedMs, bool isSlow)
    {
        var statusCode = context.Response.StatusCode;
        var logValues = new object[]
        {
        isSlow ? "*SLOW*" : "",
        context.Request.Method,
        context.Request.Path,
        statusCode,
        elapsedMs
        };

        if (statusCode >= 500)
        {
            logger.LogError(LogTemplate, logValues);
        }
        else if (statusCode >= 400)
        {
            logger.LogWarning(LogTemplate, logValues);
        }
        else
        {
            logger.LogInformation(LogTemplate, logValues);
        }
    }
}
