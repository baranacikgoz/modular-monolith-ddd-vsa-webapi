using Microsoft.AspNetCore.Http;

namespace Common.Core.Contracts.Results;

public static class SyncExtensions
{
    public static Result<TNext> Bind<T, TNext>(this Result<T> result, Func<T, Result<TNext>> next)
    {
        if (!result.IsSucceeded)
        {
            return Result<TNext>.Failed(result.Failure!);
        }

        return next(result.Value!);
    }

    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> mapperFunc)
    {
        if (!result.IsSucceeded)
        {
            return Result<TOut>.Failed(result.Failure!);
        }

        return Result<TOut>.Succeeded(mapperFunc(result.Value!));
    }

    public static TNext Match<T, TNext>(this Result<T> result, Func<T, TNext> onSuccess, Func<Failure, TNext> onFailure)
    {
        if (!result.IsSucceeded)
        {
            return onFailure(result.Failure!);
        }

        return onSuccess(result.Value!);
    }

    public static TNext Match<TNext>(this Result result, Func<TNext> onSuccess, Func<Failure, TNext> onFailure)
    {
        if (!result.IsSucceeded)
        {
            return onFailure(result.Failure!);
        }

        return onSuccess();
    }
}

public static class AsyncExtensions
{

    public static async Task<Result<TNext>> BindAsync<T, TNext>(this Result<T> result, Func<T, Task<Result<TNext>>> next)
    {
        if (!result.IsSucceeded)
        {
            return Result<TNext>.Failed(result.Failure!);
        }

        return await next(result.Value!).ConfigureAwait(false);
    }

    public static async Task<Result<TNext>> BindAsync<TNext>(this Result result, Func<Task<Result<TNext>>> next)
    {
        if (!result.IsSucceeded)
        {
            return Result<TNext>.Failed(result.Failure!);
        }

        return await next().ConfigureAwait(false);
    }

    public static async Task<Result> BindAsync(this Result result, Func<Task<Result>> next)
    {
        if (!result.IsSucceeded)
        {
            return Result.Failed(result.Failure!);
        }

        return await next().ConfigureAwait(false);
    }

    public static async Task<Result<TNext>> BindAsync<T, TNext>(this Task<Result<T>> resultTask, Func<T, Task<Result<TNext>>> next)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSucceeded)
        {
            return Result<TNext>.Failed(result.Failure!);
        }

        var nextResult = await next(result.Value!).ConfigureAwait(false);
        return nextResult;
    }

    public static async Task<Result<TNext>> BindAsync<TNext>(this Task<Result> resultTask, Func<Task<Result<TNext>>> next)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSucceeded)
        {
            return Result<TNext>.Failed(result.Failure!);
        }

        var nextResult = await next().ConfigureAwait(false);
        return nextResult;
    }

    public static async Task<Result<TNext>> BindAsync<T, TNext>(this Task<Result<T>> resultTask, Func<T, Result<TNext>> next)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSucceeded)
        {
            return Result<TNext>.Failed(result.Failure!);
        }

        return next(result.Value!);
    }

    public static async Task<Result<TNext>> BindAsync<TNext>(this Task<Result> resultTask, Func<Task<TNext>> next)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSucceeded)
        {
            return Result<TNext>.Failed(result.Failure!);
        }

        var nextResult = await next().ConfigureAwait(false);
        return Result<TNext>.Succeeded(nextResult);
    }

    public static async Task<Result<TNext>> MapAsync<TCurrent, TNext>(this Task<Result<TCurrent>> resultTask, Func<TCurrent, TNext> mapper)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSucceeded)
        {
            return Result<TNext>.Failed(result.Failure!);
        }

        var nextValue = mapper(result.Value!);
        return Result<TNext>.Succeeded(nextValue);
    }

    public static async Task<Result<TNext>> MapAsync<TCurrent, TNext>(this Task<Result<TCurrent>> resultTask, Func<TCurrent, Task<TNext>> mapper)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSucceeded)
        {
            return Result<TNext>.Failed(result.Failure!);
        }

        var nextValue = await mapper(result.Value!).ConfigureAwait(false);
        return Result<TNext>.Succeeded(nextValue);
    }

    public static async Task<Result<TNext>> MapAsync<TNext>(this Task<Result> resultTask, Func<TNext> mapper)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSucceeded)
        {
            return Result<TNext>.Failed(result.Failure!);
        }

        var nextValue = mapper();
        return Result<TNext>.Succeeded(nextValue);
    }

    public static Task<TNext> MatchAsync<T, TNext>(this Result<T> result, Func<T, Task<TNext>> onSuccess, Func<Failure, Task<TNext>> onFailure)
    {
        if (!result.IsSucceeded)
        {
            return onFailure(result.Failure!);
        }

        return onSuccess(result.Value!);
    }

    public static async Task<TNext> MatchAsync<TNext>(this Task<Result> resultTask, Func<TNext> onSuccess, Func<Failure, Task<TNext>> onFailure)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSucceeded)
        {
            return await onFailure(result.Failure!).ConfigureAwait(false);
        }

        return onSuccess();
    }
}
