using System.Net;
using Common.Application.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Npgsql;

namespace Host.Middlewares;

#pragma warning disable CA1031

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
        catch (DbUpdateConcurrencyException concurrencyException)
        {
            await HandleExceptionAsync(
                context: context,
                exception: concurrencyException,
                statusCode: (int)HttpStatusCode.Conflict,
                title: localizer[nameof(HttpStatusCode.Conflict)]);
        }
        catch (DbUpdateException dbUpdateException) when (dbUpdateException.InnerException is PostgresException postgresException
                                                          && postgresException.SqlState == PostgresErrorCodes.UniqueViolation)
        {
            await HandleExceptionAsync(
                context: context,
                exception: postgresException,
                statusCode: (int)HttpStatusCode.BadRequest,
                title: localizer[nameof(PostgresErrorCodes.UniqueViolation)]);
        }
        catch (DbUpdateException dbUpdateException)
        {
            await HandleExceptionAsync(
                context: context,
                exception: dbUpdateException,
                statusCode: (int)HttpStatusCode.InternalServerError,
                title: localizer[nameof(HttpStatusCode.InternalServerError)]);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(
                context: context,
                exception: exception,
                statusCode: (int)HttpStatusCode.InternalServerError,
                title: localizer[nameof(HttpStatusCode.InternalServerError), context.TraceIdentifier]);
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

        var details = new ProblemDetails()
        {
            Status = statusCode,
            Title = title
        };

        context.Response.StatusCode = statusCode;
        await problemDetailsService.WriteAsync(new ProblemDetailsContext()
        {
            HttpContext = context,
            Exception = exception,
            ProblemDetails = details,
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

#pragma warning restore CA1031 // Do not catch general exception types
