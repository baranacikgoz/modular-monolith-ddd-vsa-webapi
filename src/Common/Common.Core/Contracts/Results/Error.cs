using System.Net;

namespace Common.Core.Contracts.Results;

public class Error
{
    public required string Key { get; init; }
    public string? ParameterName { get; init; }
    public object? Value { get; init; }
    public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.BadRequest;
    public ICollection<string> SubErrors { get; init; } = [];

    public static Error NotFound(string parameterName, object value)
        => new()
        {
            Key = nameof(NotFound),
            ParameterName = parameterName,
            Value = value,
            StatusCode = HttpStatusCode.NotFound
        };

    public static Error NotUnique(string parameterName, object value)
        => new()
        {
            Key = nameof(NotUnique),
            ParameterName = parameterName,
            Value = value
        };
}
