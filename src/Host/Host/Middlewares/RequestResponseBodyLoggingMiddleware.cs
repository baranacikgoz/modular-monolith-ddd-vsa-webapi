using System.Buffers;
using System.Text;
using Common.Application.Options;
using Microsoft.Extensions.Options;
using Serilog;

namespace Host.Middlewares;

// Runs inside UseSerilogRequestLogging's wrapper — enriches the Serilog diagnostic context with
// request/response body content when body logging is enabled. Not a logger itself; Serilog owns
// the log event. Short-circuits immediately when both body flags are off (production default) and
// for ExcludedPathPrefixes (health/metrics/swagger/dashboard) so those never pay the capture cost.
//
// Both directions use a bounded pass-through tee: bytes flow straight to/from the real stream as
// the handler reads the request or writes the response — streaming/SSE/large payloads are NEVER
// buffered to memory or disk — while only the first N bytes are copied into a pooled buffer for
// logging. The request tee replaces EnableBuffering, which would spool the entire body to disk
// (unbounded DoS surface) just to re-read a few KB. Capture is skipped for non-text content types.
internal sealed class RequestResponseBodyLoggingMiddleware(
    IDiagnosticContext diagnosticContext,
    IOptions<RequestLoggingOptions> options
) : IMiddleware
{
    // Marker written in place of a body/query value on sensitive paths. Explicit "[REDACTED]"
    // (vs. omitting the property) tells a log reader the value was intentionally withheld rather
    // than empty or lost — and keeps request/response/query redaction behavior identical.
    internal const string RedactedMarker = "[REDACTED]";

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var opts = options.Value;

        if (!opts.LogRequestBody && !opts.LogResponseBody)
        {
            await next(context);
            return;
        }

        var path = context.Request.Path;

        // Excluded paths (health/metrics/swagger/dashboard) skip ALL body capture — not just the
        // Serilog level drop. Wrapping their response stream would lose SendFileAsync zero-copy and
        // burn pooled buffers on constantly-polled endpoints for logs that are never emitted.
        if (IsExcluded(path, opts.ExcludedPathPrefixes))
        {
            await next(context);
            return;
        }

        var method = context.Request.Method;

        // requestLoggable: a textual request body is present and logging is on. captureRequest tees
        // it verbatim; otherwise (sensitive path) we emit the redaction marker — but only when a body
        // was actually carried (ContentLength != 0), never for an empty sensitive POST.
        var requestLoggable = opts.LogRequestBody && IsLoggableContentType(context.Request.ContentType);
        var captureRequest = requestLoggable && !IsSensitive(path, method, opts.SensitiveRequestBodyPaths);
        var redactRequest = requestLoggable && !captureRequest && context.Request.ContentLength != 0;

        Stream? originalRequestBody = null;
        BoundedRequestCaptureStream? requestCapture = null;

        if (captureRequest)
        {
            originalRequestBody = context.Request.Body;
            requestCapture = new BoundedRequestCaptureStream(originalRequestBody, opts.RequestBodyLogLimitBytes);
            context.Request.Body = requestCapture;
        }

        // Sensitive responses are NOT wrapped — the bytes never touch a capture buffer. We still
        // emit the marker afterwards if the response carried a textual body worth noting.
        var responseSensitive = opts.LogResponseBody
                                && IsSensitive(path, method, opts.SensitiveResponseBodyPaths);

        Stream? originalResponseBody = null;
        BoundedCaptureStream? responseCapture = null;

        if (opts.LogResponseBody && !responseSensitive)
        {
            originalResponseBody = context.Response.Body;
            responseCapture = new BoundedCaptureStream(
                originalResponseBody, opts.ResponseBodyLogLimitBytes, context.Response);
            context.Response.Body = responseCapture;
        }

        try
        {
            await next(context);

            if (requestCapture is not null)
            {
                var body = requestCapture.GetCapturedString();
                if (!string.IsNullOrEmpty(body))
                {
                    diagnosticContext.Set("RequestBody", body);
                }
            }
            else if (redactRequest)
            {
                diagnosticContext.Set("RequestBody", RedactedMarker);
            }

            if (responseCapture is not null)
            {
                var body = responseCapture.GetCapturedString();
                if (!string.IsNullOrEmpty(body))
                {
                    diagnosticContext.Set("ResponseBody", body);
                }
            }
            else if (responseSensitive && IsLoggableContentType(context.Response.ContentType))
            {
                diagnosticContext.Set("ResponseBody", RedactedMarker);
            }
        }
        finally
        {
            if (originalRequestBody is not null)
            {
                context.Request.Body = originalRequestBody;
            }

            if (originalResponseBody is not null)
            {
                context.Response.Body = originalResponseBody;
            }

            if (requestCapture is not null)
            {
                await requestCapture.DisposeAsync();
            }

            if (responseCapture is not null)
            {
                await responseCapture.DisposeAsync();
            }
        }
    }

    // Decodes the captured byte slice as UTF-8, dropping a trailing multi-byte sequence that the
    // byte limit cut in half. Without this a body truncated mid-codepoint logs a U+FFFD replacement
    // char as its last character; trimming the partial lead/continuation bytes keeps the log clean.
    private static string DecodeUtf8Bounded(byte[] buffer, int length)
    {
        if (length == 0)
        {
            return string.Empty;
        }

        var i = length - 1;
        while (i >= 0 && (buffer[i] & 0xC0) == 0x80)
        {
            i--; // walk back over continuation bytes (10xxxxxx)
        }

        if (i >= 0)
        {
            var lead = buffer[i];
            var expected = ExpectedUtf8SequenceLength(lead);
            var have = length - i;
            if (expected > 1 && have < expected)
            {
                length = i; // drop the incomplete trailing sequence
            }
        }

        return Encoding.UTF8.GetString(buffer, 0, length);
    }

    // Expected total byte length of a UTF-8 sequence given its lead byte. Malformed/continuation
    // lead bytes fall through to 1 so a corrupt body never trims valid bytes ahead of it.
    private static int ExpectedUtf8SequenceLength(byte lead)
    {
        if (lead < 0x80)
        {
            return 1;
        }

        if ((lead & 0xE0) == 0xC0)
        {
            return 2;
        }

        if ((lead & 0xF0) == 0xE0)
        {
            return 3;
        }

        if ((lead & 0xF8) == 0xF0)
        {
            return 4;
        }

        return 1;
    }

    private static bool IsLoggableContentType(string? contentType)
    {
        if (string.IsNullOrEmpty(contentType))
        {
            return false;
        }

        return contentType.Contains("application/json", StringComparison.OrdinalIgnoreCase)
               || contentType.Contains("+json", StringComparison.OrdinalIgnoreCase)
               || contentType.Contains("application/xml", StringComparison.OrdinalIgnoreCase)
               || contentType.Contains("+xml", StringComparison.OrdinalIgnoreCase)
               || contentType.Contains("text/", StringComparison.OrdinalIgnoreCase);
    }

    private static bool IsExcluded(PathString path, IList<string> prefixes)
    {
        for (var i = 0; i < prefixes.Count; i++)
        {
            if (path.StartsWithSegments(prefixes[i], StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    internal static bool IsSensitive(PathString path, string method, IList<SensitivePathRule> rules)
    {
        for (var i = 0; i < rules.Count; i++)
        {
            var rule = rules[i];
            if (!path.StartsWithSegments(rule.Path, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (rule.Methods is null || rule.Methods.Count == 0 || MethodMatches(rule.Methods, method))
            {
                return true;
            }
        }

        return false;
    }

    private static bool MethodMatches(IList<string> methods, string method)
    {
        for (var i = 0; i < methods.Count; i++)
        {
            if (string.Equals(methods[i], method, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    // Read-side tee: every read passes through to the inner (real) request body unchanged, so the
    // handler streams the body exactly once with no buffering to memory or disk. The first `limit`
    // bytes the handler consumes are copied into a pooled buffer for logging. Unlike EnableBuffering
    // there is no second full copy and no disk spool — large/hostile bodies stay bounded.
    private sealed class BoundedRequestCaptureStream(Stream inner, int limit) : Stream
    {
        private byte[]? _buffer = ArrayPool<byte>.Shared.Rent(limit);
        private int _captured;

        public override bool CanRead => inner.CanRead;
        public override bool CanSeek => false;
        public override bool CanWrite => false;
        public override long Length => throw new NotSupportedException();

        public override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        private void Capture(ReadOnlySpan<byte> data)
        {
            if (_buffer is null || _captured >= limit || data.IsEmpty)
            {
                return;
            }

            var toCopy = Math.Min(limit - _captured, data.Length);
            data[..toCopy].CopyTo(_buffer.AsSpan(_captured));
            _captured += toCopy;
        }

        public string GetCapturedString()
        {
            return _buffer is null ? string.Empty : DecodeUtf8Bounded(_buffer, _captured);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var read = inner.Read(buffer, offset, count);
            Capture(buffer.AsSpan(offset, read));
            return read;
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            var read = await inner.ReadAsync(buffer.AsMemory(offset, count), cancellationToken);
            Capture(buffer.AsSpan(offset, read));
            return read;
        }

        public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
        {
            var read = await inner.ReadAsync(buffer, cancellationToken);
            Capture(buffer.Span[..read]);
            return read;
        }

        public override void Flush()
        {
            inner.Flush();
        }

        public override Task FlushAsync(CancellationToken cancellationToken)
        {
            return inner.FlushAsync(cancellationToken);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _buffer is not null)
            {
                ArrayPool<byte>.Shared.Return(_buffer);
                _buffer = null;
            }

            base.Dispose(disposing);
        }
    }

    // Pass-through stream: every write goes straight to the inner (real) response body so streaming
    // is preserved and memory stays bounded. The first `limit` bytes are also copied into a pooled
    // buffer for logging, but only when the response Content-Type is text-like. Capture is decided
    // lazily on the first write because Content-Type is not known until the handler sets it.
    private sealed class BoundedCaptureStream(Stream inner, int limit, HttpResponse response) : Stream
    {
        private byte[]? _buffer;
        private int _captured;
        private bool _decided;
        private bool _enabled;

        public override bool CanRead => false;
        public override bool CanSeek => false;
        public override bool CanWrite => inner.CanWrite;
        public override long Length => throw new NotSupportedException();

        public override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        private void Capture(ReadOnlySpan<byte> data)
        {
            if (!_decided)
            {
                _decided = true;
                _enabled = IsLoggableContentType(response.ContentType);
                if (_enabled)
                {
                    _buffer = ArrayPool<byte>.Shared.Rent(limit);
                }
            }

            if (!_enabled || _buffer is null || _captured >= limit || data.IsEmpty)
            {
                return;
            }

            var toCopy = Math.Min(limit - _captured, data.Length);
            data[..toCopy].CopyTo(_buffer.AsSpan(_captured));
            _captured += toCopy;
        }

        public string GetCapturedString()
        {
            return _buffer is null ? string.Empty : DecodeUtf8Bounded(_buffer, _captured);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            Capture(buffer.AsSpan(offset, count));
            inner.Write(buffer, offset, count);
        }

        public override void Write(ReadOnlySpan<byte> buffer)
        {
            Capture(buffer);
            inner.Write(buffer);
        }

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            Capture(buffer.AsSpan(offset, count));
            return inner.WriteAsync(buffer, offset, count, cancellationToken);
        }

        public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
        {
            Capture(buffer.Span);
            return inner.WriteAsync(buffer, cancellationToken);
        }

        public override void Flush()
        {
            inner.Flush();
        }

        public override Task FlushAsync(CancellationToken cancellationToken)
        {
            return inner.FlushAsync(cancellationToken);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _buffer is not null)
            {
                ArrayPool<byte>.Shared.Return(_buffer);
                _buffer = null;
            }

            base.Dispose(disposing);
        }
    }
}
