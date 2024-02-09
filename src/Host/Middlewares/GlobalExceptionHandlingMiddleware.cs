using System.Net;
using Common.Core.Interfaces;
using Common.Localization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Npgsql;

namespace Host.Middlewares;

#pragma warning disable CA1031

internal partial class GlobalExceptionHandlingMiddleware(
    ILogger<GlobalExceptionHandlingMiddleware> logger,
    IProblemDetailsFactory problemDetailsFactory,
    IStringLocalizer<ResxLocalizer> localizer
    ) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (DbUpdateConcurrencyException concurrencyException)
        {
            await HandleExceptionAsync(
                context,
                concurrencyException,
                (int)HttpStatusCode.Conflict,
                localizer[nameof(HttpStatusCode.Conflict)]);
        }
        catch (DbUpdateException dbUpdateException) when (dbUpdateException.InnerException is PostgresException postgresException
                                                          && postgresException.SqlState == PostgresErrorCodes.UniqueViolation)
        {
            await HandleExceptionAsync(
                context,
                postgresException,
                (int)HttpStatusCode.BadRequest,
                localizer[nameof(PostgresErrorCodes.UniqueViolation)]);
        }
        catch (DbUpdateException dbUpdateException)
        {
            await HandleExceptionAsync(
                context,
                dbUpdateException,
                (int)HttpStatusCode.InternalServerError,
                localizer[nameof(HttpStatusCode.InternalServerError), context.TraceIdentifier]);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(
                context,
                exception,
                (int)HttpStatusCode.InternalServerError,
                localizer[nameof(HttpStatusCode.InternalServerError), context.TraceIdentifier]);
        }
    }

    private static class PostgresErrorCodes
    {
        public const string UniqueViolation = "23505";
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode, string title)
    {
        LogError(logger, exception);

        if (context.Response.HasStarted)
        {
            LogCantWriteResponse(logger);
            return;
        }

        var problemDetails = problemDetailsFactory.Create(
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

#pragma warning restore CA1031 // Do not catch general exception types
