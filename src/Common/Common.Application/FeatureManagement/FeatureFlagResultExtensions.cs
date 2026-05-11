using Common.Domain.ResultMonad;
using Microsoft.FeatureManagement;

namespace Common.Application.FeatureManagement;

public static class FeatureFlagResultExtensions
{
    public static async Task<Result<TCurrent>> TapWhenFeatureEnabledAsync<TCurrent>(
        this Task<Result<TCurrent>> resultTask,
        IFeatureManager featureManager,
        string featureName,
        Action<TCurrent> tap)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (result.IsFailure)
        {
            return Result<TCurrent>.Failure(result.Error!);
        }

        if (await featureManager.IsEnabledAsync(featureName))
        {
            tap(result.Value!);
        }

        return result;
    }

    public static async Task<Result<TCurrent>> TapWhenFeatureEnabledAsync<TCurrent>(
        this Task<Result<TCurrent>> resultTask,
        IFeatureManager featureManager,
        string featureName,
        Func<TCurrent, Task> tap)
    {
        var result = await resultTask.ConfigureAwait(false);

        if (result.IsFailure)
        {
            return Result<TCurrent>.Failure(result.Error!);
        }

        if (await featureManager.IsEnabledAsync(featureName))
        {
            await tap(result.Value!).ConfigureAwait(false);
        }

        return result;
    }
}
