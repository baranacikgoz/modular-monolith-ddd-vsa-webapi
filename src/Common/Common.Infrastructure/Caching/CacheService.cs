using Common.Application.Caching;
using Microsoft.Extensions.Caching.Hybrid;

namespace Common.Infrastructure.Caching;

public class CacheService(HybridCache hybridCache) : ICacheService
{
    public async Task<T?> GetAsync<T>(
        string cacheKey,
        CancellationToken cancellationToken)
        => await hybridCache.GetOrCreateAsync(
            key: cacheKey,
            factory: _ => new ValueTask<T?>(result: default),
            cancellationToken: cancellationToken);

    public async Task<T> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, ValueTask<T>> factory,
        IEnumerable<string>? tags = null,
        TimeSpan? absoluteExpirationRelativeToNow = null,
        CancellationToken cancellationToken = default)
        => await hybridCache.GetOrCreateAsync(
            key: key,
            tags: tags,
            factory: factory,
            options: new HybridCacheEntryOptions()
            {
                Expiration = absoluteExpirationRelativeToNow
            },
            cancellationToken: cancellationToken);

    public async Task SetAsync<T>(
        string key,
        T value,
        IEnumerable<string>? tags = null,
        TimeSpan? absoluteExpirationRelativeToNow = null,
        CancellationToken cancellationToken = default)
        => await hybridCache.SetAsync(
            key,
            value,
            options: new HybridCacheEntryOptions()
            {
                Expiration = absoluteExpirationRelativeToNow
            },
            tags,
            cancellationToken);

    public async Task RemoveAsync(
        string key,
        CancellationToken cancellationToken = default)
        => await hybridCache.RemoveAsync(key, cancellationToken);

    public async Task RemoveByTagAsync(
        IEnumerable<string> tags,
        CancellationToken cancellationToken = default)
        => await hybridCache.RemoveByTagAsync(tags, cancellationToken);
}
