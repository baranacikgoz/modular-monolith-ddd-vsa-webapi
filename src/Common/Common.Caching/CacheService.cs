using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Common.Caching;

public class CacheService(IDistributedCache cache) : ICacheService
{
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var value = await cache.GetStringAsync(key, cancellationToken);

        if (value is null)
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(value);
    }

    public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> func, TimeSpan? absoluteExpirationRelativeToNow = null, TimeSpan? slidingExpiration = null, CancellationToken cancellationToken = default)
    {
        var value = await cache.GetStringAsync(key, cancellationToken);

        if (value is not null)
        {
            return JsonSerializer.Deserialize<T>(value)!;
        }

        var result = await func();

        if (result is null)
        {
            return default!;
        }

        await SetAsync(key, result, absoluteExpirationRelativeToNow, slidingExpiration, cancellationToken);

        return result;
    }

    public async Task<T> GetOrSetAsync<T>(string key, Func<T> func, TimeSpan? absoluteExpirationRelativeToNow = null, TimeSpan? slidingExpiration = null, CancellationToken cancellationToken = default)
    {
        var value = await cache.GetStringAsync(key, cancellationToken);

        if (value is not null)
        {
            return JsonSerializer.Deserialize<T>(value)!;
        }

        var result = func();

        if (result is null)
        {
            return default!;
        }

        await SetAsync(key, result, absoluteExpirationRelativeToNow, slidingExpiration, cancellationToken);

        return result;
    }

    public async Task<T> GetOrSetAsync<T>(string key, T value, TimeSpan? absoluteExpirationRelativeToNow = null, TimeSpan? slidingExpiration = null, CancellationToken cancellationToken = default)
    {
        var result = await cache.GetStringAsync(key, cancellationToken);

        if (result is not null)
        {
            return JsonSerializer.Deserialize<T>(result)!;
        }

        await SetAsync(key, value, absoluteExpirationRelativeToNow, slidingExpiration, cancellationToken);

        return value;
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await cache.RemoveAsync(key, cancellationToken);
    }

    private Task SetAsync<T>(string key, T value, TimeSpan? absoluteExpirationRelativeToNow = null, TimeSpan? slidingExpiration = null, CancellationToken cancellationToken = default)
    {
        var options = new DistributedCacheEntryOptions();

        if (absoluteExpirationRelativeToNow is not null)
        {
            options.AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
        }

        if (slidingExpiration is not null)
        {
            options.SlidingExpiration = slidingExpiration;
        }

        return cache.SetStringAsync(key, JsonSerializer.Serialize(value), options, token: cancellationToken);
    }
}
