using Microsoft.AspNetCore.Http;

namespace Common.Application.Extensions;

public static class ProblemDetailsServiceExtensions
{
    /// <summary>
    /// Writes a problem details body, swallowing the failure when no registered
    /// <see cref="IProblemDetailsWriter"/> can handle the response's content negotiation
    /// (e.g. an SSE/WebSocket client sending <c>Accept: text/event-stream</c>). The status
    /// code is already set by the caller before this runs, so the body is best-effort only.
    /// </summary>
    public static async Task TryWriteAsync(this IProblemDetailsService problemDetailsService, ProblemDetailsContext context)
    {
        try
        {
            await problemDetailsService.WriteAsync(context);
        }
        catch (InvalidOperationException)
        {
            // No writer could handle this response (e.g. SSE/WebSocket Accept header). Status
            // code is already set by the caller, so the body is best-effort only.
        }
    }
}
