using Common.Infrastructure.Persistence.Outbox;
using Common.IntegrationEvents;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Tests.Support;

/// <summary>
///     Drives a Notifications consumer's <c>Consume</c> directly, one DI scope per delivery exactly as MassTransit does
///     (the consumers are registered by MassTransit's assembly scan), against the real Notifications schema. The
///     ConsumeContext substitute only carries the message and the token: the inbox row and the transaction around the
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
        var context = Substitute.For<ConsumeContext<TEvent>>();
        context.Message.Returns(@event);
        context.CancellationToken.Returns(CancellationToken.None);
        await handler.Consume(context);
    }

    /// <summary>
    ///     Simulates the idempotency cache losing the key (Redis blip, other instance), so only the inbox stands between
    ///     a redelivery and a second side effect.
    /// </summary>
    public static Task ForgetAsync<THandler>(IServiceProvider root, IntegrationEvent @event)
    {
        return root.GetRequiredService<IFusionCache>()
            .RemoveAsync($"processed_event:{typeof(THandler).Name}:{@event.Id}").AsTask();
    }

    /// <summary>Unprocessed outbox rows carrying <typeparamref name="TEvent" />; the outbox processor is off in slice tests.</summary>
    public static async Task<List<TEvent>> QueuedEventsAsync<TEvent>(IServiceProvider root)
        where TEvent : IntegrationEvent
    {
        await using var scope = root.CreateAsyncScope();
        var outboxDb = scope.ServiceProvider.GetRequiredService<IOutboxDbContext>();
        var messages = await outboxDb.OutboxMessages.AsNoTracking().Where(m => !m.IsProcessed).ToListAsync();
        return messages.Select(m => m.Event).OfType<TEvent>().ToList();
    }
}
