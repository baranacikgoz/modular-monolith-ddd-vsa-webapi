namespace Common.Domain.ResultMonad;

public static class SyncExtensions
{
    public static Result<TOut> Apply<TIn, TOut>(
        this Result<Func<TIn, TOut>> result,
        Result<TIn> arg)
    {
        if (result.IsFailure)
        {
            return Result<TOut>.Failure(result.Error!);
        }

        if (arg.IsFailure)
        {
            return Result<TOut>.Failure(arg.Error!);
        }

        return Result<TOut>.Success(result.Value!(arg.Value!));
    }

    public static Result<TNext> Bind<TCurrent, TNext>(
        this Result<TCurrent> result,
        Result<TNext> next)
    {
        if (result.IsFailure)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        return Result<TNext>.Success(next.Value!);
    }

    public static Result<TNext> Bind<TCurrent, TNext>(
        this Result<TCurrent> result,
        Func<TCurrent, Result<TNext>> binder)
    {
        if (result.IsFailure)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        return binder(result.Value!);
    }

    public static Result<TNext> Bind<TCurrent, TNext>(
        this Result<TCurrent> result,
        Func<TCurrent, TNext> binder)
    {
        if (result.IsFailure)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        return Result<TNext>.Success(binder(result.Value!));
    }

    public static Result Tap(
        this Result result,
        Action action)
    {
        if (result.IsFailure)
        {
            return Result.Failure(result.Error!);
        }

        action();

        return Result.Success;
    }

    public static Result TapWhen(
        this Result result,
        Action action,
        Func<bool> when)
    {
        if (result.IsFailure)
        {
            return Result.Failure(result.Error!);
        }

        if (when())
        {
            action();
        }

        return Result.Success;
    }

    public static Result Tap(
        this Result result,
        Func<Result> func)
    {
        if (result.IsFailure)
        {
            return Result.Failure(result.Error!);
        }

        var nextResult = func();

        if (nextResult.IsFailure)
        {
            return Result.Failure(nextResult.Error!);
        }

        return result;
    }

    public static Result TapWhen(
        this Result result,
        Func<Result> func,
        Func<bool> when)
    {
        if (result.IsFailure)
        {
            return Result.Failure(result.Error!);
        }

        if (when())
        {
            var nextResult = func();

            if (nextResult.IsFailure)
            {
                return Result.Failure(nextResult.Error!);
            }
        }

        return result;
    }

    public static Result<TCurrent> Tap<TCurrent>(
        this Result<TCurrent> result,
        Action<TCurrent> action)
    {
        if (result.IsFailure)
        {
            return Result<TCurrent>.Failure(result.Error!);
        }

        action(result.Value!);

        return result;
    }

    public static Result<TCurrent> TapWhen<TCurrent>(
        this Result<TCurrent> result,
        Action<TCurrent> action,
        Func<bool> when)
    {
        if (result.IsFailure)
        {
            return Result<TCurrent>.Failure(result.Error!);
        }

        if (when())
        {
            action(result.Value!);
        }

        return result;
    }

    public static Result<TCurrent> Tap<TCurrent>(
        this Result<TCurrent> result,
        Func<TCurrent, Result> func)
    {
        if (result.IsFailure)
        {
            return Result<TCurrent>.Failure(result.Error!);
        }

        var nextResult = func(result.Value!);

        if (nextResult.IsFailure)
        {
            return Result<TCurrent>.Failure(nextResult.Error!);
        }

        return result;
    }

    public static Result<TCurrent> TapWhen<TCurrent>(
        this Result<TCurrent> result,
        Func<TCurrent, Result> func,
        Func<bool> when)
    {
        if (result.IsFailure)
        {
            return Result<TCurrent>.Failure(result.Error!);
        }

        if (when())
        {
            var nextResult = func(result.Value!);

            if (nextResult.IsFailure)
            {
                return Result<TCurrent>.Failure(nextResult.Error!);
            }
        }

        return result;
    }

    public static Result<TCurrent> Tap<TCurrent>(
        this Result<TCurrent> result,
        Func<TCurrent, Result<TCurrent>> func)
    {
        if (result.IsFailure)
        {
            return Result<TCurrent>.Failure(result.Error!);
        }

        var nextResult = func(result.Value!);

        if (nextResult.IsFailure)
        {
            return Result<TCurrent>.Failure(nextResult.Error!);
        }

        return result;
    }

    public static Result<TOut> Map<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> mapperFunc)
    {
        if (result.IsFailure)
        {
            return Result<TOut>.Failure(result.Error!);
        }

        return Result<TOut>.Success(mapperFunc(result.Value!));
    }

    public static Result<TOut> Map<TOut>(
        this Result result,
        Func<TOut> mapperFunc)
    {
        if (result.IsFailure)
        {
            return Result<TOut>.Failure(result.Error!);
        }

        return Result<TOut>.Success(mapperFunc());
    }

    public static TNext Match<TCurrent, TNext>(
        this Result<TCurrent> result,
        Func<TCurrent, TNext> onSuccess,
        Func<Error, TNext> onFailure)
    {
        if (result.IsFailure)
        {
            return onFailure(result.Error!);
        }

        return onSuccess(result.Value!);
    }

    public static TNext Match<TNext>(
        this Result result,
        Func<TNext> onSuccess,
        Func<Error, TNext> onFailure)
    {
        if (result.IsFailure)
        {
            return onFailure(result.Error!);
        }

        return onSuccess();
    }
}

public static class AsyncExtensions
{
    public static async Task<Result<TNext>> BindAsync<TCurrent, TNext>(
        this Result<TCurrent> result,
        Func<TCurrent, Task<Result<TNext>>> binder)
    {
        if (result.IsFailure)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        return await binder(result.Value!).ConfigureAwait(false);
    }

    public static async Task<Result<TCurrent>> BindAsync<TCurrent>(
        this Task<Result<TCurrent>> resultTask,
        Func<TCurrent, Task<Result>> binder)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (result.IsFailure)
        {
            return Result<TCurrent>.Failure(result.Error!);
        }

        var nextResult = await binder(result.Value!);

        if (nextResult.IsFailure)
        {
            return Result<TCurrent>.Failure(nextResult.Error!);
        }

        return Result<TCurrent>.Success(result.Value!);
    }

    public static async Task<Result<TNext>> BindAsync<TCurrent, TNext>(
        this Task<Result<TCurrent>> resultTask,
        Func<TCurrent, TNext> binder)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (result.IsFailure)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        var nextResult = binder(result.Value!);

        return Result<TNext>.Success(nextResult);
    }

    public static async Task<Result<TNext>> BindAsync<TNext>(
        this Task<Result> resultTask,
        Func<Task<Result<TNext>>> next)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (result.IsFailure)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        return await next().ConfigureAwait(false);
    }

    public static async Task<Result<TNext>> BindAsync<TCurrent, TNext>(
        this Task<Result<TCurrent>> resultTask,
        Func<TNext> next)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (result.IsFailure)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        return next();
    }

    public static async Task<Result<TNext>> BindAsync<TNext>(
        this Task<Result> resultTask,
        Func<TNext> next)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (result.IsFailure)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        return next();
    }

    public static async Task<Result<TNext>> BindAsync<TNext>(
        this Task<Result> resultTask,
        Func<Task<TNext>> next)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (result.IsFailure)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        var nextValue = await next().ConfigureAwait(false);

        return Result<TNext>.Success(nextValue);
    }

    public static async Task<Result<TNext>> BindAsync<TCurrent, TNext>(
        this Task<Result<TCurrent>> resultTask,
        Func<TCurrent, Task<Result<TNext>>> binder)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (result.IsFailure)
        {
            return Result<TNext>.Failure(result.Error!);
        }

        return await binder(result.Value!).ConfigureAwait(false);
    }

    public static async Task<Result<(T1, T2)>> CombineAsync<T1, T2>(
        this Task<Result<T1>> firstTask,
        Func<T1, Task<Result<T2>>> secondSelector)
    {
        var firstResult = await firstTask.ConfigureAwait(false);
        if (firstResult.IsFailure)
        {
            return Result<(T1, T2)>.Failure(firstResult.Error!);
        }

        var secondResult = await secondSelector(firstResult.Value!).ConfigureAwait(false);
        if (secondResult.IsFailure)
        {
            return Result<(T1, T2)>.Failure(secondResult.Error!);
        }

        return Result<(T1, T2)>.Success((firstResult.Value!, secondResult.Value!));
    }

    public static async Task<Result<(T1, T2)>> CombineAsync<T1, T2>(
        this Task<Result<T1>> firstTask,
        Func<T1, Result<T2>> secondSelector)
    {
        var firstResult = await firstTask.ConfigureAwait(false);
        if (firstResult.IsFailure)
        {
            return Result<(T1, T2)>.Failure(firstResult.Error!);
        }

        var secondResult = secondSelector(firstResult.Value!);
        if (secondResult.IsFailure)
        {
            return Result<(T1, T2)>.Failure(secondResult.Error!);
        }

        return Result<(T1, T2)>.Success((firstResult.Value!, secondResult.Value!));
    }

    public static async Task<Result<(T1, T2, T3)>> CombineAsync<T1, T2, T3>(
        this Task<Result<(T1, T2)>> firstTask,
        Func<(T1, T2), Task<Result<T3>>> thirdSelector)
    {
        var result12 = await firstTask.ConfigureAwait(false);
        if (result12.IsFailure)
        {
            return Result<(T1, T2, T3)>.Failure(result12.Error!);
        }

        var thirdResult = await thirdSelector(result12.Value!).ConfigureAwait(false);
        if (thirdResult.IsFailure)
        {
            return Result<(T1, T2, T3)>.Failure(thirdResult.Error!);
        }

        var (item1, item2) = result12.Value!;
        return Result<(T1, T2, T3)>.Success((item1, item2, thirdResult.Value!));
    }

    public static async Task<Result<(T1, T2, T3)>> CombineAsync<T1, T2, T3>(
        this Task<Result<(T1, T2)>> firstTask,
        Func<(T1, T2), Result<T3>> thirdSelector)
    {
        var result12 = await firstTask.ConfigureAwait(false);
        if (result12.IsFailure)
        {
            return Result<(T1, T2, T3)>.Failure(result12.Error!);
        }

        var thirdResult = thirdSelector(result12.Value!);
        if (thirdResult.IsFailure)
        {
            return Result<(T1, T2, T3)>.Failure(thirdResult.Error!);
        }

        var (item1, item2) = result12.Value!;
        return Result<(T1, T2, T3)>.Success((item1, item2, thirdResult.Value!));
    }

    public static async Task<Result<TCurrent>> TapAsync<TCurrent>(
        this Result<TCurrent> result,
        Func<TCurrent, Task<Result>> func)
    {
        if (result.IsFailure)
        {
            return Result<TCurrent>.Failure(result.Error!);
        }

        var nextResult = await func(result.Value!).ConfigureAwait(false);

        if (nextResult.IsFailure)
        {
            return Result<TCurrent>.Failure(nextResult.Error!);
        }

        return result;
    }

    public static async Task<Result<TCurrent>> TapAsync<TCurrent>(
        this Result<TCurrent> result,
        Func<TCurrent, Task<Result<TCurrent>>> func)
    {
        if (result.IsFailure)
        {
            return Result<TCurrent>.Failure(result.Error!);
        }

        var nextResult = await func(result.Value!).ConfigureAwait(false);

        if (nextResult.IsFailure)
        {
            return Result<TCurrent>.Failure(nextResult.Error!);
        }

        return result;
    }

    public static async Task<Result<TCurrent>> TapAsync<TCurrent>(
        this Result<TCurrent> result,
        Func<TCurrent, Task> func)
    {
        if (result.IsFailure)
        {
            return Result<TCurrent>.Failure(result.Error!);
        }

        await func(result.Value!).ConfigureAwait(false);

        return result;
    }

    public static async Task<Result<TCurrent>> TapAsync<TCurrent>(
        this Task<Result<TCurrent>> resultTask,
        Func<TCurrent, Task> func)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (result.IsFailure)
        {
            return Result<TCurrent>.Failure(result.Error!);
        }

        await func(result.Value!).ConfigureAwait(false);

        return result;
    }

    public static async Task<Result<TCurrent>> TapAsync<TCurrent>(
        this Task<Result<TCurrent>> resultTask,
        Func<TCurrent, Result> func)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (result.IsFailure)
        {
            return Result<TCurrent>.Failure(result.Error!);
        }

        var nextResult = func(result.Value!);

        if (nextResult.IsFailure)
        {
            return Result<TCurrent>.Failure(nextResult.Error!);
        }

        return result;
    }

    public static async Task<Result<TCurrent>> TapAsync<TCurrent>(
        this Task<Result<TCurrent>> resultTask,
        Func<TCurrent, Result<TCurrent>> func)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (result.IsFailure)
        {
            return Result<TCurrent>.Failure(result.Error!);
        }

        var nextResult = func(result.Value!);

        if (nextResult.IsFailure)
        {
            return Result<TCurrent>.Failure(nextResult.Error!);
        }

        return result;
    }

    public static async Task<Result<TCurrent>> TapAsync<TCurrent>(
        this Task<Result<TCurrent>> resultTask,
        Action<TCurrent> func)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (result.IsFailure)
        {
            return Result<TCurrent>.Failure(result.Error!);
        }

        func(result.Value!);

        return result;
    }

    public static async Task<Result<TCurrent>> TapWhenAsync<TCurrent>(
        this Task<Result<TCurrent>> resultTask,
        Func<TCurrent, Result> tap,
        Func<bool> when)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (result.IsFailure)
        {
            return Result<TCurrent>.Failure(result.Error!);
        }

        if (when())
        {
            var tapResult = tap(result.Value!);
            if (tapResult.IsFailure)
            {
                return tapResult.Error!;
            }
        }

        return result;
    }

    public static async Task<Result<TCurrent>> TapWhenAsync<TCurrent>(
        this Task<Result<TCurrent>> resultTask,
        Func<TCurrent, Task> tap,
        Func<bool> when)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (result.IsFailure)
        {
            return Result<TCurrent>.Failure(result.Error!);
        }

        if (when())
        {
            await tap(result.Value!).ConfigureAwait(false);
        }

        return result;
    }

    public static async Task<Result<TCurrent>> TapWhenAsync<TCurrent>(
        this Task<Result<TCurrent>> resultTask,
        Func<TCurrent, Task<Result>> tap,
        Func<bool> when)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (result.IsFailure)
        {
            return Result<TCurrent>.Failure(result.Error!);
        }

        if (when())
        {
            var nextResult = await tap(result.Value!).ConfigureAwait(false);

            if (nextResult.IsFailure)
            {
                return Result<TCurrent>.Failure(nextResult.Error!);
            }
        }

        return result;
    }

    public static async Task<Result<TOut>> MapAsync<TIn, TOut>(
        this Task<Result<TIn>> resultTask,
        Func<TIn, TOut> mapper)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (result.IsFailure)
        {
            return Result<TOut>.Failure(result.Error!);
        }

        var nextValue = mapper(result.Value!);
        return Result<TOut>.Success(nextValue);
    }

    public static async Task<Result<TOut>> MapAsync<TIn, TOut>(
        this Task<Result<TIn>> resultTask,
        Func<TIn, Task<TOut>> mapper)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (result.IsFailure)
        {
            return Result<TOut>.Failure(result.Error!);
        }

        var nextValue = await mapper(result.Value!).ConfigureAwait(false);
        return Result<TOut>.Success(nextValue);
    }

    public static async Task<Result<TOut>> MapAsync<TOut>(
        this Task<Result> resultTask,
        Func<TOut> mapper)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (result.IsFailure)
        {
            return Result<TOut>.Failure(result.Error!);
        }

        var nextValue = mapper();
        return Result<TOut>.Success(nextValue);
    }
}
