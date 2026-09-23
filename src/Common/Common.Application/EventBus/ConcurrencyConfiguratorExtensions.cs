using MassTransit;

namespace Common.Application.EventBus;

/// <summary>
///     Sets PrefetchCount and ConcurrentMessageLimit. The two are independent settings; MassTransit lets you
///     prefetch more than you process concurrently, and vice versa. The recommendation (not a requirement this
///     helper enforces) is to keep them equal unless you have a specific reason not to: a message prefetched
///     beyond the concurrency limit waits unacked for a free slot, and an unacked message older than RabbitMQ's
///     consumer_timeout (30 minutes by default) gets the channel closed and every message on it redelivered.
///     The single-argument overload is the common case: one number for both.
/// </summary>
public static class ConcurrencyConfiguratorExtensions
{
    public static void SetConcurrency(this IReceiveEndpointConfigurator endpoint, int prefetchCount, int concurrentMessageLimit)
    {
        endpoint.PrefetchCount = prefetchCount;
        endpoint.ConcurrentMessageLimit = concurrentMessageLimit;
    }

    public static void SetConcurrency(this IReceiveEndpointConfigurator endpoint, int concurrency)
    {
        endpoint.SetConcurrency(concurrency, concurrency);
    }

    public static void SetConcurrency(this IBusFactoryConfigurator bus, int prefetchCount, int concurrentMessageLimit)
    {
        bus.PrefetchCount = prefetchCount;
        bus.ConcurrentMessageLimit = concurrentMessageLimit;
    }

    public static void SetConcurrency(this IBusFactoryConfigurator bus, int concurrency)
    {
        bus.SetConcurrency(concurrency, concurrency);
    }
}
