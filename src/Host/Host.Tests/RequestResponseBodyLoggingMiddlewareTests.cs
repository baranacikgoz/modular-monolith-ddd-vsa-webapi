using System.Text;
using Common.Application.Options;
using Host.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Serilog;

namespace Host.Tests;

// Pure unit tests for RequestResponseBodyLoggingMiddleware — no host, no DB, no Serilog static.
// A fake IDiagnosticContext captures exactly what the middleware would attach to the Serilog log
// event, so we can assert the logged value (not just that the round-trip survives). Covers the
// edge cases the integration tests can't observe: UTF-8 truncation, excluded-path skip, and
// empty-body redaction suppression.
public class RequestResponseBodyLoggingMiddlewareTests
{
    // "abé" → bytes 61 62 C3 A9. A 3-byte limit slices the 2-byte 'é' (C3 A9) in half, leaving a
    // dangling lead byte C3. Naive UTF8.GetString would render it as U+FFFD; the middleware trims it.
    private static readonly byte[] AbEAcuteBytes = [0x61, 0x62, 0xC3, 0xA9];

    private sealed class FakeDiagnosticContext : IDiagnosticContext
    {
        public Dictionary<string, object?> Props { get; } = [];

        public void Set(string propertyName, object? value, bool destructureObjects = false)
        {
            Props[propertyName] = value;
        }

        public void SetException(Exception exception)
        {
        }
    }

    private static RequestResponseBodyLoggingMiddleware CreateMiddleware(
        FakeDiagnosticContext diag, RequestLoggingOptions opts)
    {
        return new RequestResponseBodyLoggingMiddleware(diag, Options.Create(opts));
    }

    private static RequestLoggingOptions DefaultOptions()
    {
        return new RequestLoggingOptions
        {
            LogRequestBody = true,
            LogResponseBody = true,
            RequestBodyLogLimitBytes = 4096,
            ResponseBodyLogLimitBytes = 4096,
            LogQueryString = false,
            ExcludedPathPrefixes = [],
        };
    }

    private static DefaultHttpContext BuildContext(
        string path, string method, byte[]? requestBody, Stream responseSink)
    {
        var context = new DefaultHttpContext();
        context.Request.Path = path;
        context.Request.Method = method;

        if (requestBody is not null)
        {
            context.Request.ContentType = "application/json";
            context.Request.ContentLength = requestBody.Length;
            context.Request.Body = new MemoryStream(requestBody);
        }

        context.Response.Body = responseSink;
        return context;
    }

    [Fact]
    public async Task RequestBody_TruncatedMidCodepoint_TrimsPartialUtf8_NoReplacementChar()
    {
        var diag = new FakeDiagnosticContext();
        var opts = DefaultOptions();
        opts.RequestBodyLogLimitBytes = 3; // slices 'é' in half

        var context = BuildContext("/products", "POST", AbEAcuteBytes, Stream.Null);

        // Handler drains the body (model binding) so the read-side tee captures it.
        static async Task Next(HttpContext ctx)
        {
            using var sink = new MemoryStream();
            await ctx.Request.Body.CopyToAsync(sink);
        }

        await CreateMiddleware(diag, opts).InvokeAsync(context, Next);

        var logged = Assert.IsType<string>(diag.Props["RequestBody"]);
        Assert.Equal("ab", logged);
        Assert.DoesNotContain('�', logged);
    }

    [Fact]
    public async Task ResponseBody_LargerThanLimit_StreamsFullBodyToClient_AndCapturesTrimmedSlice()
    {
        var diag = new FakeDiagnosticContext();
        var opts = DefaultOptions();
        opts.ResponseBodyLogLimitBytes = 3; // slices 'é' in half

        var clientSink = new MemoryStream();
        var context = BuildContext("/products", "GET", requestBody: null, responseSink: clientSink);

        static async Task Next(HttpContext ctx)
        {
            ctx.Response.ContentType = "application/json";
            await ctx.Response.Body.WriteAsync(AbEAcuteBytes);
        }

        await CreateMiddleware(diag, opts).InvokeAsync(context, Next);

        // Client received every byte despite the 3-byte log limit — proves pass-through.
        Assert.Equal(AbEAcuteBytes, clientSink.ToArray());

        var logged = Assert.IsType<string>(diag.Props["ResponseBody"]);
        Assert.Equal("ab", logged);
        Assert.DoesNotContain('�', logged);
    }

    [Fact]
    public async Task ExcludedPath_DoesNotCaptureOrWrapBodies()
    {
        var diag = new FakeDiagnosticContext();
        var opts = DefaultOptions();
        opts.ExcludedPathPrefixes.Add("/health");

        var clientSink = new MemoryStream();
        var context = BuildContext("/health/ready", "POST", AbEAcuteBytes, clientSink);

        Stream? bodyDuringHandler = null;

        async Task Next(HttpContext ctx)
        {
            bodyDuringHandler = ctx.Response.Body;
            ctx.Response.ContentType = "application/json";
            await ctx.Response.Body.WriteAsync(AbEAcuteBytes);
        }

        await CreateMiddleware(diag, opts).InvokeAsync(context, Next);

        // Nothing logged, and the response stream was never swapped for a capture tee.
        Assert.Empty(diag.Props);
        Assert.Same(clientSink, bodyDuringHandler);
        Assert.Equal(AbEAcuteBytes, clientSink.ToArray());
    }

    [Fact]
    public async Task SensitiveRequestPath_WithBody_EmitsRedactedMarker()
    {
        var diag = new FakeDiagnosticContext();
        var opts = DefaultOptions();
        opts.SensitiveRequestBodyPaths.Add(new SensitivePathRule { Path = "/tokens" });

        var context = BuildContext("/tokens", "POST", AbEAcuteBytes, Stream.Null);

        await CreateMiddleware(diag, opts).InvokeAsync(context, _ => Task.CompletedTask);

        Assert.Equal(RequestResponseBodyLoggingMiddleware.RedactedMarker, diag.Props["RequestBody"]);
    }

    [Fact]
    public async Task SensitiveRequestPath_EmptyBody_DoesNotEmitMarker()
    {
        var diag = new FakeDiagnosticContext();
        var opts = DefaultOptions();
        opts.SensitiveRequestBodyPaths.Add(new SensitivePathRule { Path = "/tokens" });

        // ContentLength 0 → no body carried → redaction marker must be suppressed.
        var context = BuildContext("/tokens", "POST", [], Stream.Null);

        await CreateMiddleware(diag, opts).InvokeAsync(context, _ => Task.CompletedTask);

        Assert.False(diag.Props.ContainsKey("RequestBody"));
    }

    [Fact]
    public async Task RequestBody_NonSensitiveTextBody_CapturedVerbatim()
    {
        var diag = new FakeDiagnosticContext();
        var opts = DefaultOptions();
        var payload = Encoding.UTF8.GetBytes("""{"name":"widget"}""");

        var context = BuildContext("/products", "POST", payload, Stream.Null);

        static async Task Next(HttpContext ctx)
        {
            using var sink = new MemoryStream();
            await ctx.Request.Body.CopyToAsync(sink);
        }

        await CreateMiddleware(diag, opts).InvokeAsync(context, Next);

        Assert.Equal("""{"name":"widget"}""", diag.Props["RequestBody"]);
    }
}
