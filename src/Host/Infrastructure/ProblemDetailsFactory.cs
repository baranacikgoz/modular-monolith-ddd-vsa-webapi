using Common.Core.Interfaces;

namespace Host.Infrastructure;

public class ProblemDetailsFactory : IProblemDetailsFactory
{
    public IResult Create(int status, string title, string type, string instance, string requestId, IEnumerable<string> errors)
        => new CustomProblemDetails
        {
            Status = status,
            Title = title,
            Type = type,
            Instance = instance,
            RequestId = requestId,
            Errors = errors
        };
}

internal class CustomProblemDetails : IResult
{
    public required int Status { get; init; }
    public required string Title { get; init; }
    public required string Type { get; init; }
    public required string Instance { get; init; }
    public required string RequestId { get; init; }
    public required IEnumerable<string> Errors { get; init; }

    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = Status;
        httpContext.Response.ContentType = "application/json";

        return httpContext.Response.WriteAsJsonAsync(this);
    }
}

