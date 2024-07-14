namespace Common.Application.Caching;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string cacheKey, CancellationToken cancellationToken);
    Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, ValueTask<T>> factory, IReadOnlyCollection<string>? tags = null, TimeSpan? absoluteExpirationRelativeToNow = null, CancellationToken cancellationToken = default);
    Task SetAsync<T>(string key, T value, IReadOnlyCollection<string>? tags = null, TimeSpan? absoluteExpirationRelativeToNow = null, CancellationToken token = default);
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    Task RemoveByTagAsync(IReadOnlyCollection<string> tags, CancellationToken cancellationToken = default);
}
