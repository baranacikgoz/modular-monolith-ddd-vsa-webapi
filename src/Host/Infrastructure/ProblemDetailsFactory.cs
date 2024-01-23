using Common.Core.Interfaces;
using Microsoft.Extensions.ObjectPool;

namespace Host.Infrastructure;

public class ProblemDetailsFactory(ObjectPool<CustomProblemDetails> problemDetailsPool) : IProblemDetailsFactory
{
    public IResult Create(int status, string title, string type, string instance, string requestId, IEnumerable<string> errors)
    {
        var problemDetails = problemDetailsPool.Get();
        try
        {
            problemDetails.ReInitialize(
                status: status,
                title: title,
                type: type,
                instance: instance,
                requestId: requestId,
                errors: errors);

            return problemDetails;
        }
        finally
        {
            problemDetailsPool.Return(problemDetails);
        }
    }
}

public class CustomProblemDetails : IResult, IResettable
{
    public int Status { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Type { get; private set; } = string.Empty;
    public string Instance { get; private set; } = string.Empty;
    public string RequestId { get; private set; } = string.Empty;
    private readonly List<string> _errors = [];
    public IReadOnlyCollection<string> Errors => _errors;

    public void ReInitialize(int status, string title, string type, string instance, string requestId, IEnumerable<string> errors)
    {
        Status = status;
        Title = title;
        Type = type;
        Instance = instance;
        RequestId = requestId;
        _errors.AddRange(errors);
    }

    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = Status;
        httpContext.Response.ContentType = "application/json";

        return httpContext.Response.WriteAsJsonAsync(this);
    }

    public bool TryReset()
    {
        _errors.Clear();
        return true;
    }
}

