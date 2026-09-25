using System.Net;
using System.Threading.RateLimiting;
using Common.Application.Options;
using Common.Infrastructure.RateLimiting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Polly;
using Polly.CircuitBreaker;
using Polly.RateLimiting;
using Polly.Registry;
using Polly.Timeout;
using StackExchange.Redis;

namespace Common.Infrastructure.Resiliency;

/// <summary>
///     One resilience pipeline per string key (a partner account, a tenant's API credential, a per-minute quota bucket),
///     each with its own rate limiter, circuit breaker and attempt timeout, so one noisy key cannot open the circuit or
///     burn the quota of another behind the same <see cref="HttpClient" />. Pipelines are built once per key and cached
///     in a <see cref="ResiliencePipelineRegistry{TKey}" />. Wrap the send with
///     <see cref="HttpClientKeyedExtensions.SendWithPipelineAsync" /> on a client registered with
///     <c>useStandardPipeline: false</c>; a rejected call surfaces as <see cref="RateLimiterRejectedException" />,
///     <see cref="BrokenCircuitException" /> or <see cref="TimeoutRejectedException" />.
///     With Redis configured (<paramref name="redis" /> registered) a queue-less profile counts its quota in Redis, so
///     every replica draws from one shared budget instead of each pod spending the full quota on its own; without Redis,
///     or with a waiting queue, the quota is per process.
/// </summary>
public sealed class KeyedResiliencePipelines(
    IOptions<ResiliencyOptions> resiliencyOptionsProvider,
    IConnectionMultiplexer? redis = null,
    ILoggerFactory? loggerFactory = null) : IDisposable
{
    private readonly ResiliencePipelineRegistry<string> _registry = new();
    private readonly List<RateLimiter> _limiters = [];
    private readonly Lock _limitersLock = new();

    /// <summary>Pipeline for <paramref name="key" /> using the profile of the same name in <see cref="ResiliencyOptions.Keyed" />.</summary>
    public ResiliencePipeline<HttpResponseMessage> GetOrAdd(string key)
    {
        if (!resiliencyOptionsProvider.Value.Keyed.TryGetValue(key, out var profile))
        {
            throw new InvalidOperationException($"No keyed resilience profile named '{key}' in ResiliencyOptions.Keyed.");
        }

        return GetOrAdd(key, profile);
    }

    /// <summary>Pipeline for <paramref name="key" />, built from <paramref name="profile" /> on first use.</summary>
    public ResiliencePipeline<HttpResponseMessage> GetOrAdd(string key, KeyedResilienceProfile profile)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        ArgumentNullException.ThrowIfNull(profile);

        return _registry.GetOrAddPipeline<HttpResponseMessage>(key, builder => Configure(builder, key, profile));
    }

    private void Configure(ResiliencePipelineBuilder<HttpResponseMessage> builder, string key, KeyedResilienceProfile profile)
    {
        var limiter = CreateLimiter(key, profile);

        lock (_limitersLock)
        {
            _limiters.Add(limiter);
        }

        // Outermost to innermost: quota first (a rejected call never counts against the circuit), then the breaker,
        // then the per-attempt timeout (a timeout counts as a failure for the breaker).
        builder
            .AddRateLimiter(new RateLimiterStrategyOptions
            {
                RateLimiter = args => limiter.AcquireAsync(1, args.Context.CancellationToken)
            })
            .AddCircuitBreaker(new CircuitBreakerStrategyOptions<HttpResponseMessage>
            {
                FailureRatio = profile.CircuitBreakerFailureRatio,
                MinimumThroughput = profile.CircuitBreakerMinimumThroughput,
                SamplingDuration = TimeSpan.FromSeconds(profile.CircuitBreakerSamplingDurationSeconds),
                BreakDuration = TimeSpan.FromSeconds(profile.CircuitBreakerBreakDurationSeconds),
                ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                    .Handle<HttpRequestException>()
                    .Handle<TimeoutRejectedException>()
                    .HandleResult(IsTransientFailure)
            })
            .AddTimeout(TimeSpan.FromSeconds(profile.AttemptTimeoutSeconds));
    }

    private RateLimiter CreateLimiter(string key, KeyedResilienceProfile profile)
    {
        var window = TimeSpan.FromMilliseconds(profile.RateLimitWindowMs);

        // The shared counter is a plain fixed window in Redis, it cannot hold callers back for the next window: a
        // profile that asks for a queue keeps its in-process limiter.
        if (redis is not null && profile.RateLimitQueueLimit == 0)
        {
            var logger = (loggerFactory ?? NullLoggerFactory.Instance).CreateLogger(typeof(KeyedResiliencePipelines).FullName!);
            return new RedisFixedWindowRateLimiter(
                redis, $"ratelimit:outbound:{key}", profile.RateLimitPermits, window, profile.RateLimitFailOpen!.Value, logger);
        }

        return new FixedWindowRateLimiter(new FixedWindowRateLimiterOptions
        {
            PermitLimit = profile.RateLimitPermits,
            Window = window,
            QueueLimit = profile.RateLimitQueueLimit,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            AutoReplenishment = true
        });
    }

    /// <summary>Same classification as the standard handler: server errors, timeouts and throttling trip the breaker.</summary>
    private static bool IsTransientFailure(HttpResponseMessage response)
    {
        return (int)response.StatusCode >= 500
               || response.StatusCode is HttpStatusCode.RequestTimeout or HttpStatusCode.TooManyRequests;
    }

    public void Dispose()
    {
        _registry.Dispose();

        lock (_limitersLock)
        {
            foreach (var limiter in _limiters)
            {
                limiter.Dispose();
            }

            _limiters.Clear();
        }
    }
}
