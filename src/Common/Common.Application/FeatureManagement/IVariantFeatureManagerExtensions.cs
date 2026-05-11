using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;

namespace Common.Application.FeatureManagement;

public static class IVariantFeatureManagerExtensions
{
    public static async Task<TVariant> GetVariantAsync<TVariant>(
        this IVariantFeatureManager featureManager,
        string featureName,
        CancellationToken cancellationToken = default)
        where TVariant : class
    {
        var variant = await featureManager.GetVariantAsync(featureName, cancellationToken);
        return variant.Configuration.Get<TVariant>()!;
    }
}
