using System.Diagnostics;
using Common.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Serilog.Context;

namespace Host.Middlewares;

internal partial class RequestResponseLoggingMiddleware(
    ILogger<RequestResponseLoggingMiddleware> logger,
    IOptions<LoggingMonitoringTracingOptions> loggingOptionsProvider
    ) : IMiddleware
{
    private readonly int _responseTimeThresholdMs = loggingOptionsProvider.Value.ResponseTimeThresholdInMs;
    private const string LogTemplate = "HTTP {Method} {Path} responded {StatusCode} in {Elapsed:0} ms";
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();

        context.Response.OnCompleted(() =>
        {
            stopwatch.Stop();
            var elapsedMs = stopwatch.Elapsed.TotalMilliseconds;
            var isSlow = elapsedMs > _responseTimeThresholdMs;

            using (LogContext.PushProperty("IsSlow", isSlow))
            {
                LogResponse(logger, context, elapsedMs, isSlow);
            }

            return Task.CompletedTask;
        });

        await next(context);
    }

    private static void LogResponse(ILogger<RequestResponseLoggingMiddleware> logger, HttpContext context, double elapsedMs, bool isSlow)
    {
        var statusCode = context.Response.StatusCode;
        var method = context.Request.Method;
        var path = context.Request.Path;

        // Log critical if response is slow, regardless of status code.
        if (isSlow)
        {
            LogCriticalResponse(logger, method, path, statusCode, elapsedMs);
        }
        else if (statusCode >= 500)
        {
            LogErrorResponse(logger, method, path, statusCode, elapsedMs);
        }
        else if (statusCode >= 400)
        {
            LogWarningResponse(logger, method, path, statusCode, elapsedMs);
        }
        else
        {
            LogInformationResponse(logger, method, path, statusCode, elapsedMs);
        }
    }

    [LoggerMessage(
        Level = LogLevel.Information,
        Message = LogTemplate)]
    private static partial void LogInformationResponse(
        ILogger logger,
        string method,
        string path,
        int statusCode,
        double elapsed);
    [LoggerMessage(
        Level = LogLevel.Warning,
        Message = LogTemplate)]
    private static partial void LogWarningResponse(
        ILogger logger,
        string method,
        string path,
        int statusCode,
        double elapsed);

    [LoggerMessage(
        Level = LogLevel.Error,
        Message = LogTemplate)]
    private static partial void LogErrorResponse(
        ILogger logger,
        string method,
        string path,
        int statusCode,
        double elapsed);

    [LoggerMessage(
        Level = LogLevel.Critical,
        Message = LogTemplate)]
    private static partial void LogCriticalResponse(
        ILogger logger,
        string method,
        string path,
        int statusCode,
        double elapsed);

}
