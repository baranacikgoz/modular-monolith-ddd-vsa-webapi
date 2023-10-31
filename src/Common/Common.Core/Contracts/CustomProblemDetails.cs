using Microsoft.AspNetCore.Http;

namespace Common.Core.Contracts;

public class CustomProblemDetails : IResult
{
    public required int Status { get; init; }
    public required string Title { get; init; }
    public required string Type { get; init; }
    public required string Instance { get; init; }
    public required string TraceId { get; init; }
    public required IEnumerable<string> Errors { get; init; }

    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = Status;
        httpContext.Response.ContentType = "application/json";

        return httpContext.Response.WriteAsJsonAsync(this);
    }
}
