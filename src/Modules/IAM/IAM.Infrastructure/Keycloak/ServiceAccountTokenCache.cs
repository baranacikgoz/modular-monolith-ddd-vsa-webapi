namespace IAM.Infrastructure.Keycloak;

/// <summary>
///     Per-instance holder for the service-account token. Singleton so every transient
///     <see cref="ServiceAccountTokenProvider" /> (typed HttpClient) in this process shares one token and one
///     refresh lock. Deliberately not shared across instances: client-credentials grants are independent, each
///     instance renews its own token once per lifetime (minutes), and a shared cache would only place a bearer
///     secret in Redis for no benefit.
/// </summary>
internal sealed class ServiceAccountTokenCache : IDisposable
{
    private readonly SemaphoreSlim _refreshLock = new(1, 1);

    public string? AccessToken { get; private set; }
    public DateTimeOffset ExpiresAt { get; private set; } = DateTimeOffset.MinValue;

    public bool IsValid(DateTimeOffset now, TimeSpan skew)
    {
        return AccessToken is not null && ExpiresAt - skew > now;
    }

    public void Set(string accessToken, DateTimeOffset expiresAt)
    {
        AccessToken = accessToken;
        ExpiresAt = expiresAt;
    }

    public void Invalidate()
    {
        AccessToken = null;
        ExpiresAt = DateTimeOffset.MinValue;
    }

    public Task WaitAsync(CancellationToken cancellationToken)
    {
        return _refreshLock.WaitAsync(cancellationToken);
    }

    public void Release()
    {
        _refreshLock.Release();
    }

    public void Dispose()
    {
        _refreshLock.Dispose();
    }
}
