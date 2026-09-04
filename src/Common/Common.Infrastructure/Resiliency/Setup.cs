using Common.Application.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Options;
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
            .ConfigurePrimaryHttpMessageHandler(sp => new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(
                    sp.GetRequiredService<IOptions<ResiliencyOptions>>().Value.PooledConnectionLifetimeMinutes),
            })
            .SetHandlerLifetime(Timeout.InfiniteTimeSpan);

        builder.AddStandardResilienceHandler().Configure((options, serviceProvider) =>
        {
            var resiliency = serviceProvider.GetRequiredService<IOptions<ResiliencyOptions>>().Value;

            // Total request timeout (outer): hard ceiling including all retries
            options.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(resiliency.TotalRequestTimeoutSeconds);

            // Retry: exponential backoff with jitter
            options.Retry.MaxRetryAttempts = resiliency.MaxRetryAttempts;
            options.Retry.Delay = TimeSpan.FromSeconds(resiliency.RetryDelaySeconds);
            options.Retry.BackoffType = DelayBackoffType.Exponential;
            options.Retry.UseJitter = true;

            // Circuit breaker: failure ratio over a sampling window, then break
            options.CircuitBreaker.SamplingDuration = TimeSpan.FromSeconds(resiliency.CircuitBreakerSamplingDurationSeconds);
            options.CircuitBreaker.FailureRatio = resiliency.CircuitBreakerFailureRatio;
            options.CircuitBreaker.MinimumThroughput = resiliency.CircuitBreakerMinimumThroughput;
            options.CircuitBreaker.BreakDuration = TimeSpan.FromSeconds(resiliency.CircuitBreakerBreakDurationSeconds);

            // Per-attempt timeout (inner)
            options.AttemptTimeout.Timeout = TimeSpan.FromSeconds(resiliency.AttemptTimeoutSeconds);

            // Allow caller to override any/all of the above
            configureResilience?.Invoke(options);
        });

        return builder;
    }
}
