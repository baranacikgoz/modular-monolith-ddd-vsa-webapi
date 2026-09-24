using System.Net;
using Common.Application.Options;
using Common.Infrastructure.Resiliency;
using Microsoft.Extensions.Options;
using Polly.CircuitBreaker;
using Polly.RateLimiting;
using Xunit;

namespace Common.Tests.Resiliency;

public class KeyedResiliencePipelinesTests
{
    private static KeyedResilienceProfile Profile(int permits = 100, int queueLimit = 0) => new()
    {
        RateLimitPermits = permits,
        RateLimitWindowMs = 60_000,
        RateLimitQueueLimit = queueLimit,
        CircuitBreakerFailureRatio = 1.0,
        CircuitBreakerMinimumThroughput = 2,
        CircuitBreakerSamplingDurationSeconds = 30,
        CircuitBreakerBreakDurationSeconds = 30,
        AttemptTimeoutSeconds = 5
    };

    private static KeyedResiliencePipelines CreateSut(Dictionary<string, KeyedResilienceProfile>? keyed = null)
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
        }));
    }

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
}
