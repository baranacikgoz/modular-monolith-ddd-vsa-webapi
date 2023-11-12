using System.Net;

namespace Common.Core.Contracts.Results;

public class Error(string key, HttpStatusCode statusCode = HttpStatusCode.BadRequest, IEnumerable<string>? errors = null)
{
    public string Key { get; } = key;
    public HttpStatusCode StatusCode { get; } = statusCode;
    public IEnumerable<string>? Errors { get; } = errors;
}

public sealed class Result
{
    private static readonly Result _success = new(null);
    public bool IsSuccess { get; }
    public Error? Error { get; }

    private Result(Error? error)
    {
        Error = error;
        IsSuccess = error is null;
    }

    public static Result Success => _success;
    public static Result Failure(Error error) => new(error);

#pragma warning disable CA2225
    public static implicit operator Result(Error error) => Failure(error);
#pragma warning restore CA2225
}

public sealed class Result<T>
{
    public T? Value { get; }
    public Error? Error { get; }
    public bool IsSuccess { get; }

    private Result(Error error) : this(default, error) { }

    private Result(T value) : this(value, null) { }

    private Result(T? value, Error? error)
    {
        Value = value;
        Error = error;
        IsSuccess = error is null;
    }

#pragma warning disable CA1000
    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Error error) => new(error);
#pragma warning restore CA1000

#pragma warning disable CA2225
    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(Error error) => Failure(error);
#pragma warning restore CA2225
}
