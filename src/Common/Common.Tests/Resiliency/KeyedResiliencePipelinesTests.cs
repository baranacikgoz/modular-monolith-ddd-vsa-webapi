using System.Net;
using Common.Application.Options;
using Common.Infrastructure.Resiliency;
using Microsoft.Extensions.Options;
using NSubstitute;
using Polly.CircuitBreaker;
using Polly.RateLimiting;
using StackExchange.Redis;
using Xunit;

namespace Common.Tests.Resiliency;

public class KeyedResiliencePipelinesTests
{
    private static KeyedResilienceProfile Profile(int permits = 100, int queueLimit = 0, bool failOpen = true) => new()
    {
        RateLimitPermits = permits,
        RateLimitWindowMs = 60_000,
        RateLimitQueueLimit = queueLimit,
        RateLimitFailOpen = failOpen,
        CircuitBreakerFailureRatio = 1.0,
        CircuitBreakerMinimumThroughput = 2,
        CircuitBreakerSamplingDurationSeconds = 30,
        CircuitBreakerBreakDurationSeconds = 30,
        AttemptTimeoutSeconds = 5
    };

    private static KeyedResiliencePipelines CreateSut(Dictionary<string, KeyedResilienceProfile>? keyed = null, IConnectionMultiplexer? redis = null)
    {
        return new KeyedResiliencePipelines(Options.Create(new ResiliencyOptions
        {
            PooledConnectionLifetimeMinutes = 15,
            TotalRequestTimeoutSeconds = 30,
            MaxRetryAttempts = 3,
            RetryDelaySeconds = 1,
            CircuitBreakerSamplingDurationSeconds = 30,
            CircuitBreakerFailureRatio = 0.1,
            CircuitBreakerMinimumThroughput = 10,
            CircuitBreakerBreakDurationSeconds = 15,
            AttemptTimeoutSeconds = 10,
            Keyed = keyed ?? []
        }), redis);
    }

    /// <summary>A Redis whose rate limit script answers with the given counter and remaining TTL (what the Lua script returns).</summary>
    private static IConnectionMultiplexer RedisCounting(long count, long pttlMs)
    {
        var database = Substitute.For<IDatabase>();
        database.ScriptEvaluateAsync(Arg.Any<string>(), Arg.Any<RedisKey[]?>(), Arg.Any<RedisValue[]?>(), Arg.Any<CommandFlags>())
            .Returns(RedisResult.Create(new[]
            {
                RedisResult.Create((RedisValue)count, ResultType.Integer),
                RedisResult.Create((RedisValue)pttlMs, ResultType.Integer)
            }));
        return RedisFor(database);
    }

    private static IConnectionMultiplexer RedisDown()
    {
        var database = Substitute.For<IDatabase>();
        database.ScriptEvaluateAsync(Arg.Any<string>(), Arg.Any<RedisKey[]?>(), Arg.Any<RedisValue[]?>(), Arg.Any<CommandFlags>())
            .Returns(Task.FromException<RedisResult>(new RedisConnectionException(ConnectionFailureType.UnableToConnect, "redis is down")));
        return RedisFor(database);
    }

    private static IConnectionMultiplexer RedisFor(IDatabase database)
    {
        var redis = Substitute.For<IConnectionMultiplexer>();
        redis.GetDatabase(Arg.Any<int>(), Arg.Any<object?>()).Returns(database);
        return redis;
    }

#pragma warning disable CA2000 // The response is handed to the pipeline's caller, which disposes it (every test wraps the result in a using).
    private static ValueTask<HttpResponseMessage> Ok() => ValueTask.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
#pragma warning restore CA2000

    [Fact]
    public async Task GetOrAdd_TwoKeys_HaveIndependentCircuitState()
    {
        using var sut = CreateSut();
        var partnerA = sut.GetOrAdd("partner-a", Profile());
        var partnerB = sut.GetOrAdd("partner-b", Profile());

        // Two server errors reach the minimum throughput with a 100% failure ratio: A's circuit opens.
        for (var i = 0; i < 2; i++)
        {
            using var failed = await partnerA.ExecuteAsync(_ => ValueTask.FromResult(new HttpResponseMessage(HttpStatusCode.BadGateway)));
        }

        await Assert.ThrowsAsync<BrokenCircuitException>(async () =>
        {
            using var _ = await partnerA.ExecuteAsync(_ => ValueTask.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));
        });

        using var ok = await partnerB.ExecuteAsync(_ => ValueTask.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));
        Assert.Equal(HttpStatusCode.OK, ok.StatusCode);
    }

    [Fact]
    public async Task GetOrAdd_RateLimit_RejectsTheCallPastThePermitCountWithinTheWindow()
    {
        using var sut = CreateSut();
        var pipeline = sut.GetOrAdd("quota", Profile(permits: 2, queueLimit: 0));

        for (var i = 0; i < 2; i++)
        {
            using var admitted = await pipeline.ExecuteAsync(_ => ValueTask.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));
            Assert.Equal(HttpStatusCode.OK, admitted.StatusCode);
        }

        await Assert.ThrowsAsync<RateLimiterRejectedException>(async () =>
        {
            using var _ = await pipeline.ExecuteAsync(_ => ValueTask.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));
        });
    }

    [Fact]
    public async Task GetOrAdd_WithRedis_RejectsWhenTheSharedCounterIsPastThePermitCount()
    {
        using var sut = CreateSut(redis: RedisCounting(count: 3, pttlMs: 4_000));
        var pipeline = sut.GetOrAdd("quota", Profile(permits: 2));

        var rejected = await Assert.ThrowsAsync<RateLimiterRejectedException>(async () =>
        {
            using var _ = await pipeline.ExecuteAsync(_ => Ok());
        });

        Assert.Equal(TimeSpan.FromMilliseconds(4_000), rejected.RetryAfter);
    }

    [Fact]
    public async Task GetOrAdd_WithRedis_AdmitsWhileTheSharedCounterIsWithinThePermitCount()
    {
        using var sut = CreateSut(redis: RedisCounting(count: 2, pttlMs: 4_000));
        var pipeline = sut.GetOrAdd("quota", Profile(permits: 2));

        using var admitted = await pipeline.ExecuteAsync(_ => Ok());

        Assert.Equal(HttpStatusCode.OK, admitted.StatusCode);
    }

    [Fact]
    public async Task GetOrAdd_WithRedisDownAndFailOpen_AdmitsTheCall()
    {
        using var sut = CreateSut(redis: RedisDown());
        var pipeline = sut.GetOrAdd("quota", Profile(failOpen: true));

        using var admitted = await pipeline.ExecuteAsync(_ => Ok());

        Assert.Equal(HttpStatusCode.OK, admitted.StatusCode);
    }

    [Fact]
    public async Task GetOrAdd_WithRedisDownAndFailClosed_RejectsTheCall()
    {
        using var sut = CreateSut(redis: RedisDown());
        var pipeline = sut.GetOrAdd("quota", Profile(failOpen: false));

        await Assert.ThrowsAsync<RateLimiterRejectedException>(async () =>
        {
            using var _ = await pipeline.ExecuteAsync(_ => Ok());
        });
    }

    [Fact]
    public async Task GetOrAdd_WithRedisAndAWaitingQueue_KeepsTheQuotaInProcess()
    {
        var redis = RedisCounting(count: 99, pttlMs: 1_000);
        using var sut = CreateSut(redis: redis);
        var pipeline = sut.GetOrAdd("queued", Profile(permits: 1, queueLimit: 1));

        using var admitted = await pipeline.ExecuteAsync(_ => Ok());

        Assert.Equal(HttpStatusCode.OK, admitted.StatusCode);
        redis.DidNotReceive().GetDatabase(Arg.Any<int>(), Arg.Any<object?>());
    }

    [Fact]
    public void GetOrAdd_SameKeyTwice_ReturnsTheSamePipeline()
    {
        using var sut = CreateSut();

        var first = sut.GetOrAdd("same", Profile());
        var second = sut.GetOrAdd("same", Profile());

        Assert.Same(first, second);
    }

    [Fact]
    public void GetOrAdd_ByNameFromOptions_UsesTheConfiguredProfileAndRejectsUnknownNames()
    {
        using var sut = CreateSut(new Dictionary<string, KeyedResilienceProfile>(StringComparer.Ordinal) { ["configured"] = Profile() });

        Assert.NotNull(sut.GetOrAdd("configured"));
        Assert.Throws<InvalidOperationException>(() => sut.GetOrAdd("missing"));
    }

    [Fact]
    public void Validator_KeyedNull_Fails()
    {
        var options = new ResiliencyOptions
        {
            PooledConnectionLifetimeMinutes = 15,
            TotalRequestTimeoutSeconds = 30,
            MaxRetryAttempts = 3,
            RetryDelaySeconds = 1,
            CircuitBreakerSamplingDurationSeconds = 30,
            CircuitBreakerFailureRatio = 0.1,
            CircuitBreakerMinimumThroughput = 10,
            CircuitBreakerBreakDurationSeconds = 15,
            AttemptTimeoutSeconds = 10,
            Keyed = null!
        };

        var result = new ResiliencyOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(ResiliencyOptions.Keyed));
    }

    [Fact]
    public void Validator_KeyedProfileOutOfRange_Fails()
    {
        var validator = new ResiliencyOptionsValidator();
        var options = new ResiliencyOptions
        {
            PooledConnectionLifetimeMinutes = 15,
            TotalRequestTimeoutSeconds = 30,
            MaxRetryAttempts = 3,
            RetryDelaySeconds = 1,
            CircuitBreakerSamplingDurationSeconds = 30,
            CircuitBreakerFailureRatio = 0.1,
            CircuitBreakerMinimumThroughput = 10,
            CircuitBreakerBreakDurationSeconds = 15,
            AttemptTimeoutSeconds = 10,
            Keyed = new Dictionary<string, KeyedResilienceProfile>(StringComparer.Ordinal) { ["bad"] = Profile(permits: 0) }
        };

        var result = validator.Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage.Contains("Keyed[bad].RateLimitPermits", StringComparison.Ordinal));
    }

    [Fact]
    public void Validator_KeyedProfileWithoutFailOpen_Fails()
    {
        var profile = Profile();
        profile.RateLimitFailOpen = null;
        var options = new ResiliencyOptions
        {
            PooledConnectionLifetimeMinutes = 15,
            TotalRequestTimeoutSeconds = 30,
            MaxRetryAttempts = 3,
            RetryDelaySeconds = 1,
            CircuitBreakerSamplingDurationSeconds = 30,
            CircuitBreakerFailureRatio = 0.1,
            CircuitBreakerMinimumThroughput = 10,
            CircuitBreakerBreakDurationSeconds = 15,
            AttemptTimeoutSeconds = 10,
            Keyed = new Dictionary<string, KeyedResilienceProfile>(StringComparer.Ordinal) { ["missing-flag"] = profile }
        };

        var result = new ResiliencyOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage.Contains("Keyed[missing-flag].RateLimitFailOpen", StringComparison.Ordinal));
    }
}
