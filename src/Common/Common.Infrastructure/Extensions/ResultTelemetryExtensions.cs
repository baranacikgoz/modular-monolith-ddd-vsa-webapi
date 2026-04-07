using System.Diagnostics;
using System.Runtime.CompilerServices;
using Common.Domain.ResultMonad;

namespace Common.Infrastructure.Extensions;

/// <summary>
///     Functional pipeline extensions for enriching OpenTelemetry Activity spans
///     from Result monad outcomes. Designed for zero-allocation when tracing is disabled.
/// </summary>
public static class ResultTelemetryExtensions
{
    /// <summary>
    ///     Records the result status on the given Activity (if any).
    ///     On success, sets <see cref="ActivityStatusCode.Ok" />.
    ///     On failure, sets <see cref="ActivityStatusCode.Error" /> with the error key as a tag.
    /// </summary>
    public static Result<T> TapActivity<T>(this Result<T> result, Activity? activity)
    {
        if (activity is null)
        {
            return result;
        }

        if (result.IsFailure)
        {
            activity.SetStatus(ActivityStatusCode.Error, result.Error!.Key);
            activity.SetTag("error.type", result.Error.Key);
        }
        else
        {
            activity.SetStatus(ActivityStatusCode.Ok);
        }

        return result;
    }

    /// <summary>
    ///     Async variant: Records the result status on the given Activity after awaiting the pipeline.
    /// </summary>
    public static async Task<Result<T>> TapActivityAsync<T>(this Task<Result<T>> resultTask, Activity? activity)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.TapActivity(activity);
    }

    /// <summary>
    ///     Records the non-generic result status on the given Activity (if any).
    /// </summary>
    public static Result TapActivity(this Result result, Activity? activity)
    {
        if (activity is null)
        {
            return result;
        }

        if (result.IsFailure)
        {
            activity.SetStatus(ActivityStatusCode.Error, result.Error!.Key);
            activity.SetTag("error.type", result.Error.Key);
        }
        else
        {
            activity.SetStatus(ActivityStatusCode.Ok);
        }

        return result;
    }

    /// <summary>
    ///     Async variant for non-generic Result.
    /// </summary>
    public static async Task<Result> TapActivityAsync(this Task<Result> resultTask, Activity? activity)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.TapActivity(activity);
    }

    /// <summary>
    ///     Starts a new Activity span from the given <see cref="ActivitySource" /> using the caller member name
    ///     as the operation name. Returns null when tracing is disabled (zero allocation).
    /// </summary>
    public static Activity? StartActivityForCaller(
        this ActivitySource source,
        [CallerMemberName] string operationName = "")
    {
        return source.StartActivity(operationName);
    }
}
