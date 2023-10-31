using Common.Core.Contracts;
using Microsoft.Extensions.Localization;

namespace Host.Middlewares;

public class ExceptionHandlingMiddleware(
    ILogger<ExceptionHandlingMiddleware> logger
    ) : IMiddleware
{

#pragma warning disable CA1031
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unhandled exception occured.");

            var problemDetails = GenerateProblemResponse(context, exception);

            await problemDetails.ExecuteAsync(context);
        }
    }
#pragma warning restore CA1031
    private static CustomProblemDetails GenerateProblemResponse(HttpContext context, Exception exception)
    {
        var localizer = context.RequestServices.GetRequiredService<IStringLocalizer<ExceptionHandlingMiddleware>>();

        var problemDetails = new CustomProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = localizer["Beklenmeyen bir hata oluştu. Hata'nın izini ({0}) bizimle paylaşarak anında çözülmesini sağlayabilirsiniz.", context.TraceIdentifier],
            Type = exception.GetType().FullName ?? string.Empty,
            Instance = context.Request.Path,
            TraceId = context.TraceIdentifier,
            Errors = Enumerable.Empty<string>()
        };

        return problemDetails;
    }
}

