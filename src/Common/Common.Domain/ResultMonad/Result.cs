namespace Common.Domain.ResultMonad;

public sealed class Result : IResult
{
    // Do not remove this constructor. It is used by MediatR Validation Pipeline.
    public Result()
    {
    }

    private Result(Error? error) => Error = error;
    public bool IsFailure => Error is not null;

    public static Result Success { get; } = new(null);
    public Error? Error { get; set; }

    public static Result Failure(Error error)
    {
        return new Result(error);
    }

    public static Result Create()
    {
        return Success;
    }

    public static async Task<Result> CreateAsync(Func<Task> taskToAwaitValue)
    {
        await taskToAwaitValue();
        return Success;
    }

    public static async Task<Result> CreateAsync(Func<Task<Result>> taskToAwaitValue)
    {
        var result = await taskToAwaitValue();
        if (result.IsFailure)
        {
            return Failure(result.Error!);
        }

        return Success;
    }

#pragma warning disable CA2225
    public static implicit operator Result(Error error)
    {
        return Failure(error);
    }

    public static implicit operator bool(Result result)
    {
        return !result.IsFailure;
    }
#pragma warning restore CA2225
}

public sealed class Result<T> : IResult
{
    // Do not remove this constructor. It is used by MediatR Validation Pipeline.
    public Result()
    {
    }

    private Result(Error error) : this(default, error)
    {
    }

    private Result(T value) : this(value, null)
    {
    }

    private Result(T? value, Error? error)
    {
        Value = value;
        Error = error;
    }

    public T? Value { get; }
    public bool IsFailure => Error is not null;
    public Error? Error { get; set; }

#pragma warning disable CA1000
    public static async Task<Result<T>> CreateAsync(Func<Task<T>> taskToAwaitValue)
    {
        var value = await taskToAwaitValue();
        return Success(value);
    }

    public static async Task<Result<T>> CreateAsync(Func<Task<T?>> taskToAwaitValue, Error errorIfValueNull)
    {
        var value = await taskToAwaitValue();
        return value is null ? Failure(errorIfValueNull) : Success(value);
    }

    public static Result<T> Create(Func<T> funcToGetValue)
    {
        var value = funcToGetValue();
        return Success(value);
    }

    public static Result<T> Create(Func<T?> funcToGetValue, Error errorIfValueNull)
    {
        var value = funcToGetValue();
        return value is null ? Failure(errorIfValueNull) : Success(value);
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(value);
    }

    public static Result<T> Failure(Error error)
    {
        return new Result<T>(error);
    }
#pragma warning restore CA1000

#pragma warning disable CA2225
    public static implicit operator Result<T>(T value)
    {
        return Success(value);
    }

    public static implicit operator Result<T>(Error error)
    {
        return Failure(error);
    }

    public static implicit operator Result(Result<T> result)
    {
        return result.IsFailure ? Result.Failure(result.Error!) : Result.Success;
    }

    public static implicit operator bool(Result<T> result)
    {
        return !result.IsFailure;
    }
#pragma warning restore CA2225
}
