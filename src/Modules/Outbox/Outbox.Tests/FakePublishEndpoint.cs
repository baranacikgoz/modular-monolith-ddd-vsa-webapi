using MassTransit;

namespace Outbox.Tests;

// Test double for IPublishEndpoint. OutboxProcessor.ProcessBatchAsync only ever calls the
// Publish(object, Type, CancellationToken) overload, so that's the only member with real behavior —
// everything else on this (large) MassTransit interface is unused here and throws if ever hit.
// Registered as a singleton in OutboxProcessorTestFactory, replacing the real bus, so tests can force
// deterministic publish successes/failures without a RabbitMQ broker.
internal sealed class FakePublishEndpoint : IPublishEndpoint
{
    // Settable per test (tests share one instance via the IClassFixture-scoped DI container) — reassign
    // at the start of every [Fact] rather than relying on any default carried over from a prior test.
    public Func<CancellationToken, Task> OnPublish { get; set; } = _ => Task.CompletedTask;

    public Task Publish(object message, Type messageType, CancellationToken cancellationToken = default)
        => OnPublish(cancellationToken);

    public Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : class
        => throw new NotSupportedException("Unused by OutboxProcessor.");

    public Task Publish<T>(T message, IPipe<PublishContext<T>> publishPipe, CancellationToken cancellationToken = default)
        where T : class
        => throw new NotSupportedException("Unused by OutboxProcessor.");

    public Task Publish<T>(T message, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default)
        where T : class
        => throw new NotSupportedException("Unused by OutboxProcessor.");

    public Task Publish(object message, CancellationToken cancellationToken = default)
        => throw new NotSupportedException("Unused by OutboxProcessor.");

    public Task Publish(object message, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default)
        => throw new NotSupportedException("Unused by OutboxProcessor.");

    public Task Publish(object message, Type messageType, IPipe<PublishContext> publishPipe,
        CancellationToken cancellationToken = default)
        => throw new NotSupportedException("Unused by OutboxProcessor.");

    public Task Publish<T>(object values, CancellationToken cancellationToken = default) where T : class
        => throw new NotSupportedException("Unused by OutboxProcessor.");

    public Task Publish<T>(object values, IPipe<PublishContext<T>> publishPipe, CancellationToken cancellationToken = default)
        where T : class
        => throw new NotSupportedException("Unused by OutboxProcessor.");

    public Task Publish<T>(object values, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default)
        where T : class
        => throw new NotSupportedException("Unused by OutboxProcessor.");

    public ConnectHandle ConnectPublishObserver(IPublishObserver observer)
        => throw new NotSupportedException("Unused by OutboxProcessor.");
}
