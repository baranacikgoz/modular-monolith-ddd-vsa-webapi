using System.Net;
using Common.Application.Localization;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Npgsql;

namespace Host.Middlewares;

internal sealed partial class GlobalExceptionHandlingMiddleware(
    IProblemDetailsService problemDetailsService,
    ILogger<GlobalExceptionHandlingMiddleware> logger,
    IStringLocalizer<ResxLocalizer> localizer
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
                localizer[nameof(DbUpdateConcurrencyException)]);
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
                    ? $"{localizer[columnName]} {localizer[nameof(UniqueConstraintException)]}"
                    : localizer[nameof(UniqueConstraintException)]);
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
                    ? $"{localizer[columnName]} {localizer[nameof(CannotInsertNullException)]}"
                    : localizer[nameof(CannotInsertNullException)]);
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
                    ? $"{localizer[columnName]} {localizer[nameof(MaxLengthExceededException)]}"
                    : localizer[nameof(MaxLengthExceededException)]);
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
                    ? $"{localizer[columnName]} {localizer[nameof(NumericOverflowException)]}"
                    : localizer[nameof(NumericOverflowException)]);
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
                    ? $"{localizer[columnName]} {localizer[nameof(ReferenceConstraintException)]}"
                    : localizer[nameof(ReferenceConstraintException)]);
        }
        catch (DbUpdateException
               ex) // Should not happen because we use EntityFramework.Exceptions and ".UseExceptionProcessor()"in the DbContext setup, but just in case
        {
            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.InternalServerError,
                localizer[nameof(HttpStatusCode.InternalServerError)]);
        }
        catch (BadHttpRequestException ex)
        {
            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.BadRequest,
                localizer[nameof(BadHttpRequestException)]);
        }
        catch (OperationCanceledException ex)
        {
            await HandleExceptionAsync(
                context,
                ex,
                StatusCodes.Status499ClientClosedRequest, // Client Closed Request
                localizer["ClientClosedRequest"]);
        }
#pragma warning disable CA1031 // Do not catch general exception types
        catch (Exception ex)
        {
            await HandleExceptionAsync(
                context,
                ex,
                (int)HttpStatusCode.InternalServerError,
                localizer[nameof(HttpStatusCode.InternalServerError)]);
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
