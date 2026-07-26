using System.Diagnostics;
using System.Threading.RateLimiting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Common.Infrastructure.RateLimiting;

/// <summary>
///     Fixed-window limiter backed by a single atomic Redis Lua script (INCR + conditional PEXPIRE), so
///     the count is shared across every replica instead of living in-process per pod. Mirrors the same
///     atomicity approach as Notifications' RedisOtpService.
/// </summary>
internal sealed partial class RedisFixedWindowRateLimiter : RateLimiter
{
    // KEYS[1] = window key, ARGV[1] = window duration in ms.
    // PEXPIRE only fires on the first hit in the window — calling it unconditionally would re-arm the
    // TTL on every request under sustained traffic and the key would never expire.
    private const string Script =
        """
        local count = redis.call('INCR', KEYS[1])
        if count == 1 then
            redis.call('PEXPIRE', KEYS[1], ARGV[1])
        end
        return {count, redis.call('PTTL', KEYS[1])}
        """;

    private static readonly RateLimitLease SuccessLease = new FixedWindowLease(isAcquired: true, retryAfter: null);

    private readonly IConnectionMultiplexer _redis;
    private readonly string _key;
    private readonly int _permitLimit;
    private readonly TimeSpan _window;
    private readonly bool _failOpen;
    private readonly ILogger _logger;

    private long _lastAccess = Stopwatch.GetTimestamp();

    public RedisFixedWindowRateLimiter(
        IConnectionMultiplexer redis, string key, int permitLimit, TimeSpan window, bool failOpen, ILogger logger)
    {
        if (permitLimit <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(permitLimit), permitLimit, "Must be greater than 0.");
        }

        if (window <= TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(window), window, "Must be greater than TimeSpan.Zero.");
        }

        _redis = redis;
        _key = key;
        _permitLimit = permitLimit;
        _window = window;
        _failOpen = failOpen;
        _logger = logger;
    }

    public override TimeSpan? IdleDuration => Stopwatch.GetElapsedTime(Interlocked.Read(ref _lastAccess));

    public override RateLimiterStatistics? GetStatistics() => null;

    protected override RateLimitLease AttemptAcquireCore(int permitCount) =>
        // Redis requires network I/O; the sync path must never block a request thread. Failing here
        // unconditionally forces every acquire through AcquireAsyncCore, matching the middleware's own
        // fallback order (it tries AttemptAcquire first, only awaits AcquireAsync if that fails).
        new FixedWindowLease(isAcquired: false, retryAfter: null);

    protected override async ValueTask<RateLimitLease> AcquireAsyncCore(int permitCount, CancellationToken cancellationToken)
    {
        if (permitCount > _permitLimit)
        {
            throw new ArgumentOutOfRangeException(nameof(permitCount), permitCount,
                $"{permitCount} permit(s) exceeds the permit limit of {_permitLimit}.");
        }

        Interlocked.Exchange(ref _lastAccess, Stopwatch.GetTimestamp());

        try
        {
            var db = _redis.GetDatabase();
            var raw = await db.ScriptEvaluateAsync(
                Script, [(RedisKey)_key], [(RedisValue)(long)_window.TotalMilliseconds]);
            var result = (RedisResult[])raw!;
            var count = (long)result[0];
            var pttlMs = (long)result[1];

            if (count <= _permitLimit)
            {
                return SuccessLease;
            }

            var retryAfter = pttlMs > 0 ? TimeSpan.FromMilliseconds(pttlMs) : _window;
            return new FixedWindowLease(isAcquired: false, retryAfter);
        }
        catch (RedisException ex)
        {
            LogRedisUnavailable(_logger, _key, _failOpen ? "open" : "closed", ex);
            return _failOpen ? SuccessLease : new FixedWindowLease(isAcquired: false, retryAfter: null);
        }
    }

    [LoggerMessage(Level = LogLevel.Warning, Message = "Redis rate limiter unavailable for key {Key}, failing {Mode}.")]
    private static partial void LogRedisUnavailable(ILogger logger, string key, string mode, Exception exception);

    private sealed class FixedWindowLease(bool isAcquired, TimeSpan? retryAfter) : RateLimitLease
    {
        public override bool IsAcquired { get; } = isAcquired;

        public override IEnumerable<string> MetadataNames
        {
            get
            {
                if (retryAfter is not null)
                {
                    yield return MetadataName.RetryAfter.Name;
                }
            }
        }

        public override bool TryGetMetadata(string metadataName, out object? metadata)
        {
            if (retryAfter is not null && metadataName == MetadataName.RetryAfter.Name)
            {
                metadata = retryAfter.Value;
                return true;
            }

            metadata = null;
            return false;
        }
    }
}
