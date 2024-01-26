using Common.Core.Contracts;
using Common.Core.Interfaces;
using Common.Localization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Host.Middlewares;

internal partial class GlobalExceptionHandlingMiddleware(
    ILogger<GlobalExceptionHandlingMiddleware> loggerAccessor,
    IProblemDetailsFactory problemDetailsFactoryAccessor,
    IStringLocalizer<ResxLocalizer> localizerAccessor
    ) : IExceptionHandler
{

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        switch (exception)
        {
            case DbUpdateConcurrencyException concurrencyException:
                await HandleExceptionAsync(
                    httpContext,
                    concurrencyException,
                    StatusCodes.Status409Conflict,
                    localizerAccessor["Başka bir kullanıcı sizden önce davrandı. Lütfen tekrar deneyin."]);
                return true;

            default:
                await HandleExceptionAsync(
                    httpContext,
                    exception,
                    StatusCodes.Status500InternalServerError,
                    localizerAccessor["Beklenmeyen bir hata oluştu. Hata'nın izini ({0}) bizimle paylaşarak anında çözülmesini sağlayabilirsiniz.", httpContext.TraceIdentifier]);
                return true;
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode, string title)
    {
        LogError(loggerAccessor, exception);

        if (context.Response.HasStarted)
        {
            LogCantWriteResponse(loggerAccessor);
            return;
        }

        var problemDetails = problemDetailsFactoryAccessor.Create(
            status: statusCode,
            title: title,
            type: exception.GetType().FullName ?? string.Empty,
            instance: context.Request.Path,
            requestId: context.TraceIdentifier,
            errors: Enumerable.Empty<string>());

        await problemDetails.ExecuteAsync(context);
    }

    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "Unhandled exception occured.")]
    private static partial void LogError(ILogger logger, Exception exception);

    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "Response has already started, can't write response.")]
    private static partial void LogCantWriteResponse(ILogger logger);
}

