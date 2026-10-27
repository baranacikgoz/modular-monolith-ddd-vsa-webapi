using Common.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using ZiggyCreatures.Caching.Fusion;

namespace Products.Tests.Support;

/// <summary>
///     Drives a Products consumer's <c>Consume</c> directly, one DI scope per delivery exactly as MassTransit does, against
///     the real Products schema. The ConsumeContext substitute only carries the message and the token: the inbox row and the
///     side effect are what is under test, not the transport.
/// </summary>
internal static class ConsumerHarness
{
    public static async Task DeliverAsync<THandler, TEvent>(IServiceProvider root, TEvent @event)
        where THandler : class, IConsumer<TEvent>
        where TEvent : IntegrationEvent
    {
        await using var scope = root.CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<THandler>();
        await handler.Consume(ContextFor(@event));
    }

    /// <summary>Delivers to a handler the test built itself (for instance around a failing store).</summary>
    public static Task DeliverToAsync<TEvent>(IConsumer<TEvent> handler, TEvent @event)
        where TEvent : IntegrationEvent
    {
        return handler.Consume(ContextFor(@event));
    }

    /// <summary>
    ///     Simulates the idempotency cache losing the key (Redis blip, other instance), so only the inbox and the
    ///     handler's own guards stand between a redelivery and a second side effect.
    /// </summary>
    public static Task ForgetAsync<THandler>(IServiceProvider root, IntegrationEvent @event)
    {
        return root.GetRequiredService<IFusionCache>()
            .RemoveAsync($"processed_event:{typeof(THandler).Name}:{@event.Id}").AsTask();
    }

    private static ConsumeContext<TEvent> ContextFor<TEvent>(TEvent @event)
        where TEvent : IntegrationEvent
    {
        var context = Substitute.For<ConsumeContext<TEvent>>();
        context.Message.Returns(@event);
        context.CancellationToken.Returns(CancellationToken.None);
        return context;
    }
}
