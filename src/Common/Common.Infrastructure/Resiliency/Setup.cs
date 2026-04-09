using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Polly;

namespace Common.Infrastructure.Resiliency;

/// <summary>
/// Centralized resilience infrastructure for 3rd-party HTTP service integrations.
/// Provides production-grade retry, circuit breaker, and timeout policies via
/// Microsoft.Extensions.Http.Resilience (Polly v8 underneath).
/// </summary>
public static class Setup
{
    /// <summary>
    /// Registers a typed HttpClient with production-grade resilience policies.
    /// Uses Microsoft.Extensions.Http.Resilience (Polly v8 underneath) to provide a 5-layer pipeline:
    /// Total Request Timeout → Retry → Circuit Breaker → Attempt Timeout → Rate Limiter.
    /// </summary>
    /// <typeparam name="TClient">The service interface (e.g., ICaptchaService).</typeparam>
    /// <typeparam name="TImplementation">The concrete implementation (e.g., ReCaptchaService).</typeparam>
    /// <param name="services">The service collection.</param>
    /// <param name="configureClient">Action to configure the HttpClient (e.g., BaseAddress).</param>
    /// <param name="configureResilience">Optional action to override default resilience options.</param>
    /// <returns>The IHttpClientBuilder for further chaining.</returns>
    public static IHttpClientBuilder AddResilientHttpClient<TClient, TImplementation>(
        this IServiceCollection services,
        Action<HttpClient> configureClient,
        Action<HttpStandardResilienceOptions>? configureResilience = null)
        where TClient : class
        where TImplementation : class, TClient
    {
        var builder = services
            .AddHttpClient<TClient, TImplementation>(configureClient)
            .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(15),
            })
            .SetHandlerLifetime(Timeout.InfiniteTimeSpan);

        builder.AddStandardResilienceHandler(options =>
        {
            // Total request timeout (outer): 30s — hard ceiling including all retries
            options.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(30);

            // Retry: 3 attempts, exponential backoff with jitter, median 1s delay
            options.Retry.MaxRetryAttempts = 3;
            options.Retry.Delay = TimeSpan.FromSeconds(1);
            options.Retry.BackoffType = DelayBackoffType.Exponential;
            options.Retry.UseJitter = true;

            // Circuit breaker: 10% failure rate over 30s window, break for 15s
            options.CircuitBreaker.SamplingDuration = TimeSpan.FromSeconds(30);
            options.CircuitBreaker.FailureRatio = 0.1;
            options.CircuitBreaker.MinimumThroughput = 10;
            options.CircuitBreaker.BreakDuration = TimeSpan.FromSeconds(15);

            // Per-attempt timeout (inner): 10s
            options.AttemptTimeout.Timeout = TimeSpan.FromSeconds(10);

            // Allow caller to override any/all of the above
            configureResilience?.Invoke(options);
        });

        return builder;
    }
}
