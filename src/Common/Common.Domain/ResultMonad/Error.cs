using System.Net;

namespace Common.Domain.ResultMonad;

public class Error
{
    public required string Key { get; init; }
    public string? ParameterName { get; init; }
    public object? Value { get; init; }
    public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.BadRequest;
    public ICollection<string> SubErrors { get; init; } = [];

    public static Error NotFound(string parameterName, object? value = null)
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

    /// <summary>
    /// Represents the situation where user is authorized to access/operate only his/her own resources but tries someone else's resources.
    /// </summary>
    /// <param name="parameterName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Error NotOwned(string parameterName, object value)
        => new()
        {
            Key = nameof(NotOwned),
            ParameterName = parameterName,
            Value = value
        };

    /// <summary>
    /// Represents the situation where value to change is the same as before.
    /// For example if a product's price is 5, and if a new request try to change it to 5 again, you may use this error.
    /// </summary>
    /// <param name="parameterName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Error SameValue(string parameterName, object value)
        => new()
        {
            Key = nameof(SameValue),
            ParameterName = parameterName,
            Value = value
        };
}
