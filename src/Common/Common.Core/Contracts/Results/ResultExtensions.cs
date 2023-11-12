namespace Common.Core.Contracts.Results;

public static class SyncExtensions
{
    public static Result<TNext> Bind<TCurrent, TNext>(this Result<TCurrent> result, Result<TNext> next)
    {
        if (!result.IsSuccess)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        return next;
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
    public static async Task<Result<TNext>> BindAsync<TCurrent, TNext>(this Result<TCurrent> result, Func<TCurrent, Task<Result<TNext>>> binder)
    {
        if (!result.IsSuccess)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        return await binder(result.Value!).ConfigureAwait(false);
    }

    public static async Task<Result<TNext>> BindAsync<TNext>(
        this Task<Result> resultTask,
        Task<Result<TNext>> next)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        return await next.ConfigureAwait(false);
    }

    public static async Task<Result<TNext>> BindAsync<TNext>(this Task<Result> resultTask, Task<TNext> next)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        return await next.ConfigureAwait(false);
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

}
