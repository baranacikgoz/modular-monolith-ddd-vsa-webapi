using Common.Application.Localization.Resources;
using Common.Application.Options;
using EntityFramework.Exceptions.Common;
using Host.Middlewares;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

    private const int RetryAfterSeconds = 2;

    private static async Task<(CapturingLogger Logger, int StatusCode, HttpResponse Response)> InvokeAsync(Exception thrown)
    {
        var logger = new CapturingLogger();
        var middleware = new GlobalExceptionHandlingMiddleware(
            Substitute.For<IProblemDetailsService>(),
            logger,
            Substitute.For<IResxLocalizer>(),
            Options.Create(new InterModuleRequestOptions
            {
                TimeoutSeconds = 10,
                DependencyUnavailableRetryAfterSeconds = RetryAfterSeconds
            }));
        var context = new DefaultHttpContext();

        await middleware.InvokeAsync(context, _ => throw thrown);

        return (logger, context.Response.StatusCode, context.Response);
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
        var (logger, statusCode, _) = await InvokeAsync(thrown);

        Assert.Equal(expectedStatusCode, statusCode);
        Assert.Equal([LogLevel.Warning], logger.Levels);
    }

    // Endpoint awaiting another module's answer (IInterModuleRequestClient) and the request times out or the
    // handler faults: the caller did nothing wrong and the dependency may recover, so 503 with a Retry-After hint.
    [Fact]
    public async Task InvokeAsync_InterModuleRequestTimeout_Returns503WithRetryAfter()
    {
        var (logger, statusCode, response) = await InvokeAsync(new RequestTimeoutException("request-id"));

        Assert.Equal(StatusCodes.Status503ServiceUnavailable, statusCode);
        Assert.Equal(RetryAfterSeconds.ToString(System.Globalization.CultureInfo.InvariantCulture), response.Headers.RetryAfter.ToString());
        Assert.Equal([LogLevel.Error], logger.Levels);
    }

    [Fact]
    public async Task InvokeAsync_UnhandledException_LogsError()
    {
        var (logger, statusCode, _) = await InvokeAsync(new InvalidOperationException("boom"));

        Assert.Equal(StatusCodes.Status500InternalServerError, statusCode);
        Assert.Equal([LogLevel.Error], logger.Levels);
    }
}
