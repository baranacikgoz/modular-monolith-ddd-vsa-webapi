using System.Globalization;
using System.Net;
using Common.Application.Extensions;
using Common.Application.Localization.Resources;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Host.Middlewares;

internal sealed partial class GlobalExceptionHandlingMiddleware(
    IProblemDetailsService problemDetailsService,
    ILogger<GlobalExceptionHandlingMiddleware> logger,
    IResxLocalizer localizer
) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.Conflict,
                nameof(localizer.DbUpdateConcurrencyException),
                localizer.DbUpdateConcurrencyException);
        }
        catch (UniqueConstraintException ex)
        {
            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.Conflict,
                nameof(localizer.UniqueConstraintException),
                localizer.UniqueConstraintException);
        }
        catch (CannotInsertNullException ex)
        {
            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.BadRequest,
                nameof(localizer.CannotInsertNullException),
                localizer.CannotInsertNullException);
        }
        catch (MaxLengthExceededException ex)
        {
            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.BadRequest,
                nameof(localizer.MaxLengthExceededException),
                localizer.MaxLengthExceededException);
        }
        catch (NumericOverflowException ex)
        {
            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.BadRequest,
                nameof(localizer.NumericOverflowException),
                localizer.NumericOverflowException);
        }
        catch (ReferenceConstraintException ex)
        {
            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.BadRequest,
                nameof(localizer.ReferenceConstraintException),
                localizer.ReferenceConstraintException);
        }
        catch (DbUpdateException
               ex) // Should not happen because we use EntityFramework.Exceptions and ".UseExceptionProcessor()"in the DbContext setup, but just in case
        {
            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.InternalServerError,
                nameof(localizer.InternalServerError),
                localizer.InternalServerError);
        }
        catch (BadHttpRequestException ex)
        {
            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.BadRequest,
                nameof(localizer.BadHttpRequestException),
                localizer.BadHttpRequestException);
        }
        catch (OperationCanceledException ex)
        {
            // Request aborted by the client mid-request. The connection is already gone, so there is nothing to write back
            // and this is not a server fault. Logging at Error pollutes logs with phantom failures
            // (TaskCanceledException derives from OperationCanceledException), so log at Information
            // and return without touching the dead response.
            if (context.RequestAborted.IsCancellationRequested)
            {
                LogRequestAborted(logger);
                return;
            }

            await HandleExceptionAsync(
                context,
                ex,
                StatusCodes.Status499ClientClosedRequest, // Client Closed Request
                nameof(localizer.ClientClosedRequest),
                localizer.ClientClosedRequest);
        }
#pragma warning disable CA1031 // Do not catch general exception types
        catch (Exception ex)
        {
            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.InternalServerError,
                nameof(localizer.InternalServerError),
                localizer.InternalServerError);
        }
#pragma warning restore CA1031 // Do not catch general exception types
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode, string errorKey, string title)
    {
        // A 4xx is the client's mistake, not a server fault: Error level would pollute error logs and alerts.
        if (statusCode >= StatusCodes.Status500InternalServerError)
        {
            LogError(logger, exception);
        }
        else
        {
            LogClientError(logger, exception, statusCode);
        }

        if (context.Response.HasStarted)
        {
            LogCantWriteResponse(logger);
            return;
        }

        var formattedTitle = string.Format(CultureInfo.CurrentCulture, title, context.TraceIdentifier);
        var details = new ProblemDetails { Status = statusCode, Title = formattedTitle };

        // Same envelope as a failed Result (ResultToResponseTransformer): a client never branches on error source.
        details.AddErrorKey(errorKey).AddErrors(null, []);

        context.Response.StatusCode = statusCode;
        await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = context, Exception = exception, ProblemDetails = details
        });
    }

    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "Unhandled exception occurred.")]
    private static partial void LogError(ILogger logger, Exception exception);

    [LoggerMessage(
        Level = LogLevel.Warning,
        Message = "Request failed with a client error, status code {StatusCode}.")]
    private static partial void LogClientError(ILogger logger, Exception exception, int statusCode);

    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "Response has already started, can't write response.")]
    private static partial void LogCantWriteResponse(ILogger logger);

    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Request aborted by client before completion.")]
    private static partial void LogRequestAborted(ILogger logger);
}
