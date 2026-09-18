using Common.Application.Localization.Resources;
using EntityFramework.Exceptions.Common;
using Host.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Host.Tests;

// Pure unit tests for GlobalExceptionHandlingMiddleware: no host, no DB. A capturing logger records
// the level of every entry, so a client mistake (4xx) can be told apart from a server fault (5xx).
public class GlobalExceptionHandlingMiddlewareTests
{
    private sealed class CapturingLogger : ILogger<GlobalExceptionHandlingMiddleware>
    {
        public List<LogLevel> Levels { get; } = [];

        public IDisposable? BeginScope<TState>(TState state)
            where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            Levels.Add(logLevel);
        }
    }

    private static async Task<(CapturingLogger Logger, int StatusCode)> InvokeAsync(Exception thrown)
    {
        var logger = new CapturingLogger();
        var middleware = new GlobalExceptionHandlingMiddleware(
            Substitute.For<IProblemDetailsService>(),
            logger,
            Substitute.For<IResxLocalizer>());
        var context = new DefaultHttpContext();

        await middleware.InvokeAsync(context, _ => throw thrown);

        return (logger, context.Response.StatusCode);
    }

    public static TheoryData<Exception, int> ClientErrors()
    {
        return new TheoryData<Exception, int>
        {
            { new BadHttpRequestException("malformed"), StatusCodes.Status400BadRequest },
            { new UniqueConstraintException(), StatusCodes.Status409Conflict },
            { new MaxLengthExceededException(), StatusCodes.Status400BadRequest },
        };
    }

    [Theory]
    [MemberData(nameof(ClientErrors))]
    public async Task InvokeAsync_ClientError_LogsWarningNotError(Exception thrown, int expectedStatusCode)
    {
        var (logger, statusCode) = await InvokeAsync(thrown);

        Assert.Equal(expectedStatusCode, statusCode);
        Assert.Equal([LogLevel.Warning], logger.Levels);
    }

    [Fact]
    public async Task InvokeAsync_UnhandledException_LogsError()
    {
        var (logger, statusCode) = await InvokeAsync(new InvalidOperationException("boom"));

        Assert.Equal(StatusCodes.Status500InternalServerError, statusCode);
        Assert.Equal([LogLevel.Error], logger.Levels);
    }
}
