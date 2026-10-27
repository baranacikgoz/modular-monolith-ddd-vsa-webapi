using Common.Application.Options;
using Common.Infrastructure.Resiliency;
using Inventory.Domain.WarehouseConnections;
using Inventory.Infrastructure.Gateway;
using Microsoft.Extensions.Options;

namespace Inventory.Tests.Support;

/// <summary>
///     Per-connection pipelines for gateway unit tests, with the same profile shape as resiliency.json. The permits, the
///     window and the breaker threshold are the arguments, so a quota or a circuit is exercised without waiting out the
///     real interval.
/// </summary>
internal static class TestWarehouseResilience
{
    public static WarehouseResiliencePipelines Pipelines(
        int jsonApiPermits = 20, int formApiPermits = 5, int breakerMinimumThroughput = 10, int windowMs = 10000)
    {
        var options = Options.Create(new ResiliencyOptions
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
            Keyed = new Dictionary<string, KeyedResilienceProfile>(StringComparer.Ordinal)
            {
                [WarehouseResiliencePipelines.ProfileNameOf(WarehouseProvider.JsonApi)] = Profile(jsonApiPermits, windowMs, breakerMinimumThroughput),
                [WarehouseResiliencePipelines.ProfileNameOf(WarehouseProvider.FormApi)] = Profile(formApiPermits, windowMs, breakerMinimumThroughput)
            }
        });

        // Owned by the returned wrapper for the life of one test; the process-wide registry in the host is the DI
        // singleton, this one only holds a few in-memory limiters.
#pragma warning disable CA2000
        return new WarehouseResiliencePipelines(new KeyedResiliencePipelines(options), options);
#pragma warning restore CA2000
    }

    private static KeyedResilienceProfile Profile(int permits, int windowMs, int breakerMinimumThroughput)
    {
        return new KeyedResilienceProfile
        {
            RateLimitPermits = permits,
            RateLimitWindowMs = windowMs,
            RateLimitQueueLimit = 0,
            RateLimitFailOpen = true,
            CircuitBreakerFailureRatio = 0.5,
            CircuitBreakerMinimumThroughput = breakerMinimumThroughput,
            CircuitBreakerSamplingDurationSeconds = 30,
            CircuitBreakerBreakDurationSeconds = 15,
            AttemptTimeoutSeconds = 10
        };
    }

    /// <summary>An active connection that never touches the database: the gateway tests only need its id, provider and address.</summary>
    public static WarehouseConnection Connection(
        WarehouseProvider provider = WarehouseProvider.JsonApi, byte[]? encryptedApiCredential = null, Uri? baseUrl = null)
    {
        return WarehouseConnection.Create(
            DefaultIdType.CreateVersion7(),
            provider,
            baseUrl ?? new Uri("https://warehouse.example-partner.local/api/"),
            encryptedApiCredential ?? [1, 2, 3],
            [4, 5, 6]);
    }
}
