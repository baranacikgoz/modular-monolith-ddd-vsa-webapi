using System.Net;

namespace Common.Core.Contracts.Results;

public class Error
{
    public Error(
        string key,
        IEnumerable<object>? arguments = null,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest,
        IEnumerable<string>? errors = null)
    {
        Key = key;
        StatusCode = statusCode;
        Errors = errors;
    }
    public string Key { get; }
    public IEnumerable<object>? Arguments { get; }
    public HttpStatusCode StatusCode { get; }
    public IEnumerable<string>? Errors { get; }
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
    public static async Task<Result<T>> CreateAsync(Func<Task<T?>> taskToAwaitValue, Error errorIfValueNull)
    {
        var value = await taskToAwaitValue();
        return value is null ? Failure(errorIfValueNull) : Success(value);
    }

    public static Result<T> Create(Func<T?> funcToGetValue, Error errorIfValueNull)
    {
        var value = funcToGetValue();
        return value is null ? Failure(errorIfValueNull) : Success(value);
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Error error) => new(error);
#pragma warning restore CA1000

#pragma warning disable CA2225
    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(Error error) => Failure(error);
    public static implicit operator Result(Result<T> result) => result.IsSuccess ? Result.Success : Result.Failure(result.Error!);
#pragma warning restore CA2225
}
