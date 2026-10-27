using Common.Application.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Products.Infrastructure.Caching;

internal static class ProductsSearchCacheExtensions
{
    /// <summary>Read-through with the configured default entry options (fail-safe, factory timeouts) and only the duration
    /// overridden to <see cref="ProductsOptions.SearchCacheSeconds" />. Zero disables the cache and always runs the
    /// query.</summary>
    public static async Task<T> GetOrSetSearchAsync<T>(
        this IFusionCache cache, string key, ProductsOptions options, Func<CancellationToken, Task<T>> factory,
        CancellationToken cancellationToken)
    {
        if (options.SearchCacheSeconds == 0)
        {
            return await factory(cancellationToken);
        }

        return await cache.GetOrSetAsync<T>(
            key,
            (_, ct) => factory(ct),
            entry => entry.SetDuration(TimeSpan.FromSeconds(options.SearchCacheSeconds!.Value)),
            token: cancellationToken);
    }
}
