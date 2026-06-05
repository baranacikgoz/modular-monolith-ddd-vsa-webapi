using System.Net;
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
                localizer.DbUpdateConcurrencyException);
        }
        catch (UniqueConstraintException ex)
        {
            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.Conflict,
                localizer.UniqueConstraintException);
        }
        catch (CannotInsertNullException ex)
        {
            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.BadRequest,
                localizer.CannotInsertNullException);
        }
        catch (MaxLengthExceededException ex)
        {
            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.BadRequest,
                localizer.MaxLengthExceededException);
        }
        catch (NumericOverflowException ex)
        {
            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.BadRequest,
                localizer.NumericOverflowException);
        }
        catch (ReferenceConstraintException ex)
        {
            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.BadRequest,
                localizer.ReferenceConstraintException);
        }
        catch (DbUpdateException
               ex) // Should not happen because we use EntityFramework.Exceptions and ".UseExceptionProcessor()"in the DbContext setup, but just in case
        {
            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.InternalServerError,
                localizer.InternalServerError);
        }
        catch (BadHttpRequestException ex)
        {
            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.BadRequest,
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
                localizer.ClientClosedRequest);
        }
#pragma warning disable CA1031 // Do not catch general exception types
        catch (Exception ex)
        {
            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.InternalServerError,
                localizer.InternalServerError);
        }
#pragma warning restore CA1031 // Do not catch general exception types
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode, string title)
    {
        LogError(logger, exception);

        if (context.Response.HasStarted)
        {
            LogCantWriteResponse(logger);
            return;
        }

        var details = new ProblemDetails { Status = statusCode, Title = title };

        context.Response.StatusCode = statusCode;
        await problemDetailsService.WriteAsync(new ProblemDetailsContext
        {
            HttpContext = context, Exception = exception, ProblemDetails = details
        });
    }

    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "Unhandled exception occured.")]
    private static partial void LogError(ILogger logger, Exception exception);

    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "Response has already started, can't write response.")]
    private static partial void LogCantWriteResponse(ILogger logger);

    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Request aborted by client before completion.")]
    private static partial void LogRequestAborted(ILogger logger);
}
