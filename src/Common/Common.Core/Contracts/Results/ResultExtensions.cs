namespace Common.Core.Contracts.Results;

public static class SyncExtensions
{
    public static Result<TNext> Bind<T, TNext>(this Result<T> result, Func<T, Result<TNext>> next)
    {
        if (!result.IsSuccess)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        return next(result.Value!);
    }

    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> mapperFunc)
    {
        if (!result.IsSuccess)
        {
            return Result<TOut>.Failure(result.Error!);
        }

        return Result<TOut>.Success(mapperFunc(result.Value!));
    }

    public static TNext Match<T, TNext>(this Result<T> result, Func<T, TNext> onSuccess, Func<Error, TNext> onFailure)
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

    public static async Task<Result<TNext>> BindAsync<T, TNext>(this Result<T> result, Func<T, Task<Result<TNext>>> next)
    {
        if (!result.IsSuccess)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        return await next(result.Value!).ConfigureAwait(false);
    }

    public static async Task<Result<TNext>> BindAsync<TNext>(this Result result, Func<Task<Result<TNext>>> next)
    {
        if (!result.IsSuccess)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        return await next().ConfigureAwait(false);
    }

    public static async Task<Result> BindAsync(this Result result, Func<Task<Result>> next)
    {
        if (!result.IsSuccess)
        {
            return Result.Failure(result.Error!);
        }

        return await next().ConfigureAwait(false);
    }

    public static async Task<Result<TNext>> BindAsync<T, TNext>(this Task<Result<T>> resultTask, Func<T, Task<Result<TNext>>> next)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        var nextResult = await next(result.Value!).ConfigureAwait(false);
        return nextResult;
    }

    public static async Task<Result<TNext>> BindAsync<TNext>(this Task<Result> resultTask, Func<Task<Result<TNext>>> next)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        var nextResult = await next().ConfigureAwait(false);
        return nextResult;
    }

    public static async Task<Result<TNext>> BindAsync<T, TNext>(this Task<Result<T>> resultTask, Func<T, Result<TNext>> next)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        return next(result.Value!);
    }

    public static async Task<Result<TNext>> BindAsync<TNext>(this Task<Result> resultTask, Func<Task<TNext>> next)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        var nextResult = await next().ConfigureAwait(false);
        return Result<TNext>.Success(nextResult);
    }

    public static async Task<Result<TNext>> MapAsync<TCurrent, TNext>(this Task<Result<TCurrent>> resultTask, Func<TCurrent, TNext> mapper)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        var nextValue = mapper(result.Value!);
        return Result<TNext>.Success(nextValue);
    }

    public static async Task<Result<TNext>> MapAsync<TCurrent, TNext>(this Task<Result<TCurrent>> resultTask, Func<TCurrent, Task<TNext>> mapper)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        var nextValue = await mapper(result.Value!).ConfigureAwait(false);
        return Result<TNext>.Success(nextValue);
    }

    public static async Task<Result<TNext>> MapAsync<TNext>(this Task<Result> resultTask, Func<TNext> mapper)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        var nextValue = mapper();
        return Result<TNext>.Success(nextValue);
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
