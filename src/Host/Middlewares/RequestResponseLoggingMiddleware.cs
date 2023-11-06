using Serilog.Context;

namespace Host.Middlewares;

public partial class RequestResponseLoggingMiddleware(
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

        if (statusCode >= 500)
        {
            LogErrorResponse(logger, isSlow ? "*SLOW*" : "", context.Request.Method, context.Request.Path, statusCode, elapsedMs);
        }
        else if (statusCode >= 400)
        {
            LogWarningResponse(logger, isSlow ? "*SLOW*" : "", context.Request.Method, context.Request.Path, statusCode, elapsedMs);
        }
        else
        {
            LogInformationResponse(logger, isSlow ? "*SLOW*" : "", context.Request.Method, context.Request.Path, statusCode, elapsedMs);

        }
    }

    [LoggerMessage(
        Level = LogLevel.Information,
        Message = LogTemplate)]
    private static partial void LogInformationResponse(
        ILogger logger,
        string prefix,
        string method,
        string path,
        int statusCode,
        double elapsed);

    [LoggerMessage(
        Level = LogLevel.Warning,
        Message = LogTemplate)]
    private static partial void LogWarningResponse(
        ILogger logger,
        string prefix,
        string method,
        string path,
        int statusCode,
        double elapsed);

    [LoggerMessage(
        Level = LogLevel.Error,
        Message = LogTemplate)]
    private static partial void LogErrorResponse(
        ILogger logger,
        string prefix,
        string method,
        string path,
        int statusCode,
        double elapsed);

}
