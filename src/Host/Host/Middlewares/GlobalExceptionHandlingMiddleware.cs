using System.Net;
using Common.Application.Localization.Resources;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Npgsql;

namespace Host.Middlewares;

internal sealed partial class GlobalExceptionHandlingMiddleware(
    IProblemDetailsService problemDetailsService,
    ILogger<GlobalExceptionHandlingMiddleware> logger,
    IResxLocalizer localizer,
    IStringLocalizer<ResxLocalizer> stringLocalizer
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
            string? columnName = null;
            if (ex.ConstraintProperties is { Count: > 0 })
            {
                columnName = ex.ConstraintProperties[0];
            }

            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.BadRequest,
                columnName is not null
                    ? $"{stringLocalizer[columnName]} {localizer.UniqueConstraintException}"
                    : localizer.UniqueConstraintException);
        }
        catch (CannotInsertNullException ex)
        {
            string? columnName = null;
            if (ex.InnerException is PostgresException e && !string.IsNullOrEmpty(e.ColumnName))
            {
                columnName = e.ColumnName;
            }

            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.BadRequest,
                columnName is not null
                    ? $"{stringLocalizer[columnName]} {localizer.CannotInsertNullException}"
                    : localizer.CannotInsertNullException);
        }
        catch (MaxLengthExceededException ex)
        {
            string? columnName = null;
            if (ex.InnerException is PostgresException e && !string.IsNullOrEmpty(e.ColumnName))
            {
                columnName = e.ColumnName;
            }

            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.BadRequest,
                columnName is not null
                    ? $"{stringLocalizer[columnName]} {localizer.MaxLengthExceededException}"
                    : localizer.MaxLengthExceededException);
        }
        catch (NumericOverflowException ex)
        {
            string? columnName = null;
            if (ex.InnerException is PostgresException e && !string.IsNullOrEmpty(e.ColumnName))
            {
                columnName = e.ColumnName;
            }

            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.BadRequest,
                columnName is not null
                    ? $"{stringLocalizer[columnName]} {localizer.NumericOverflowException}"
                    : localizer.NumericOverflowException);
        }
        catch (ReferenceConstraintException ex)
        {
            string? columnName = null;
            if (ex.InnerException is PostgresException e && !string.IsNullOrEmpty(e.ColumnName))
            {
                columnName = e.ColumnName;
            }

            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.BadRequest,
                columnName is not null
                    ? $"{stringLocalizer[columnName]} {localizer.ReferenceConstraintException}"
                    : localizer.ReferenceConstraintException);
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
}
