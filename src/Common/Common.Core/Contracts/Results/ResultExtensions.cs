namespace Common.Core.Contracts.Results;

public static class SyncExtensions
{
    public static Result<TNext> Bind<TCurrent, TNext>(this Result<TCurrent> result, Result<TNext> next)
    {
        if (!result.IsSuccess)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        return Result<TNext>.Success(next.Value!);
    }

    public static Result<TNext> Bind<TCurrent, TNext>(this Result<TCurrent> result, Func<TCurrent, Result<TNext>> binder)
    {
        if (!result.IsSuccess)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        return binder(result.Value!);
    }
    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> mapperFunc)
    {
        if (!result.IsSuccess)
        {
            return Result<TOut>.Failure(result.Error!);
        }

        return Result<TOut>.Success(mapperFunc(result.Value!));
    }

    public static Result<TOut> Map<TOut>(this Result result, Func<TOut> mapperFunc)
    {
        if (!result.IsSuccess)
        {
            return Result<TOut>.Failure(result.Error!);
        }

        return Result<TOut>.Success(mapperFunc());
    }

    public static TNext Match<TCurrent, TNext>(this Result<TCurrent> result, Func<TCurrent, TNext> onSuccess, Func<Error, TNext> onFailure)
    {
        if (!result.IsSuccess)
        {
            return onFailure(result.Error!);
        }

        return onSuccess(result.Value!);
    }

    public static TNext Match<TNext>(this Result result, Func<TNext> onSuccess, Func<Error, TNext> onFailure)
    {
        if (!result.IsSuccess)
        {
            return onFailure(result.Error!);
        }

        return onSuccess();
    }
}

public static class AsyncExtensions
{
    public static Task<Result<T>> ToTask<T>(this Result<T> result)
        => Task.FromResult(result);
    public static async Task<Result<TNext>> BindAsync<TCurrent, TNext>(this Result<TCurrent> result, Task<Result<TNext>> next)
    {
        if (!result.IsSuccess)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        var nextResult = await next.ConfigureAwait(false);
        return nextResult;
    }

    public static async Task<Result<TNext>> BindAsync<TCurrent, TNext>(this Result<TCurrent> result, Func<TCurrent, Task<Result<TNext>>> binder)
    {
        if (!result.IsSuccess)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        return await binder(result.Value!).ConfigureAwait(false);
    }

    public static async Task<Result> BindAsync<TCurrent>(this Result<TCurrent> result, Func<TCurrent, Task<Result>> binder)
    {
        if (!result.IsSuccess)
        {
            return Result.Failure(result.Error!);
        }

        return await binder(result.Value!).ConfigureAwait(false);
    }

    public static async Task<Result<TNext>> BindAsync<TCurrent, TNext>(this Task<Result<TCurrent>> resultTask, Task<Result<TNext>> next)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        var nextResult = await next.ConfigureAwait(false);
        return nextResult;
    }

    public static async Task<Result<TNext>> BindAsync<TCurrent, TNext>(this Task<Result<TCurrent>> resultTask, Task<TNext> next)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        var nextValue = await next.ConfigureAwait(false);
        return nextValue;
    }

    public static async Task<Result<TNext>> BindAsync<TNext>(this Task<Result> resultTask, Task<Result<TNext>> next)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        var nextResult = await next.ConfigureAwait(false);
        return nextResult;
    }

    public static async Task<Result> BindAsync(this Task<Result> resultTask, Task next)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            return Result.Failure(result.Error!);
        }

        await next.ConfigureAwait(false);

        return result;
    }

    public static async Task<Result<TNext>> BindAsync<TNext>(this Task<Result> resultTask, Task<TNext> next)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        var nextValue = await next.ConfigureAwait(false);
        return nextValue;
    }

    public static async Task<Result<TCurrent>> BindAsync<TCurrent>(this Task<Result<TCurrent>> resultTask, Task next)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            return Result<TCurrent>.Failure(result.Error!);
        }

        await next.ConfigureAwait(false);
        return Result<TCurrent>.Success(result.Value!);
    }

    public static async Task<Result<TNext>> BindAsync<TCurrent, TNext>(
    this Task<Result<TCurrent>> resultTask,
    Func<TCurrent, Task<Result<TNext>>> binder)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        return await binder(result.Value!).ConfigureAwait(false);
    }

    public static async Task<Result<TOut>> MapAsync<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, TOut> mapper)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            return Result<TOut>.Failure(result.Error!);
        }

        var nextValue = mapper(result.Value!);
        return nextValue;
    }

    public static async Task<Result<TOut>> MapAsync<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, Task<TOut>> mapper)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            return Result<TOut>.Failure(result.Error!);
        }

        var nextValue = await mapper(result.Value!).ConfigureAwait(false);
        return nextValue;
    }

    public static async Task<Result<TOut>> MapAsync<TOut>(this Task<Result> resultTask, Func<TOut> mapper)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            return Result<TOut>.Failure(result.Error!);
        }

        var nextValue = mapper();
        return Result<TOut>.Success(nextValue);
    }

    public static async Task<Result<TOut>> MapAsync<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TOut> mapper)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            return Result<TOut>.Failure(result.Error!);
        }

        var nextValue = mapper();
        return Result<TOut>.Success(nextValue);
    }

    public static Task<TNext> MatchAsync<T, TNext>(this Result<T> result, Func<T, Task<TNext>> onSuccess, Func<Error, Task<TNext>> onFailure)
    {
        if (!result.IsSuccess)
        {
            return onFailure(result.Error!);
        }

        return onSuccess(result.Value!);
    }

    public static async Task<TNext> MatchAsync<TNext>(this Task<Result> resultTask, Func<TNext> onSuccess, Func<Error, Task<TNext>> onFailure)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            return await onFailure(result.Error!).ConfigureAwait(false);
        }

        return onSuccess();
    }
}
