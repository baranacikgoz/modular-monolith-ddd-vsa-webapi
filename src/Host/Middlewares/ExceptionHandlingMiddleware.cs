using Common.Core.Contracts;
using Common.Core.Interfaces;
using Microsoft.Extensions.Localization;

namespace Host.Middlewares;

// Using IServiceProvider instead of the services' itself becuase success cases generally outnumber failure cases,
// and there are no services dependency for succes case here.
// Injected services will only be used for failure cases, so we don't eagerly load them.
// Using IServiceProvider is generally considered as bad practice and we lose explicitness of the code, but there is a tradeoff here.
internal partial class ExceptionHandlingMiddleware(IServiceProvider serviceProvider) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<ExceptionHandlingMiddleware>>();
            LogError(logger, exception);

            if (context.Response.HasStarted)
            {
                LogCantWriteResponse(logger, exception);
                return;
            }

            var problemDetailsFactory = serviceProvider.GetRequiredService<IProblemDetailsFactory>();
            var localizer = context.RequestServices.GetRequiredService<IStringLocalizer<ExceptionHandlingMiddleware>>();

            var problemDetails = problemDetailsFactory.Create(
                status: StatusCodes.Status500InternalServerError,
                title: localizer["Beklenmeyen bir hata oluştu. Hata'nın izini ({0}) bizimle paylaşarak anında çözülmesini sağlayabilirsiniz.", context.TraceIdentifier],
                type: exception.GetType().FullName ?? string.Empty,
                instance: context.Request.Path,
                requestId: context.TraceIdentifier,
                errors: Enumerable.Empty<string>()
            );

            await problemDetails.ExecuteAsync(context);
        }
    }

    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "Unhandled exception occured.")]
    private static partial void LogError(ILogger logger, Exception exception);

    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "Response has already started, can't write response.")]
    private static partial void LogCantWriteResponse(ILogger logger, Exception exception);
}

