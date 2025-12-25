namespace Common.Application.Caching;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string cacheKey, CancellationToken cancellationToken);

    Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, ValueTask<T>> factory,
        IEnumerable<string>? tags = null, TimeSpan? absoluteExpirationRelativeToNow = null,
        CancellationToken cancellationToken = default);

    Task SetAsync<T>(string key, T value, IEnumerable<string>? tags = null,
        TimeSpan? absoluteExpirationRelativeToNow = null, CancellationToken cancellationToken = default);

    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    Task RemoveByTagAsync(IEnumerable<string> tags, CancellationToken cancellationToken = default);
}
