using Common.Infrastructure.EventBus;
using Common.IntegrationEvents;
using Xunit;

namespace Common.Tests.EventBus;

public sealed class IntegrationEventOutboxTests
{
    [Fact]
    public void Drain_SameTypeAndKey_KeepsOnlyTheFirst()
    {
        var outbox = new IntegrationEventOutbox();
        var first = new Trigger("a");
        outbox.Collect(first);
        outbox.Collect(new Trigger("a"));
        outbox.Collect(new Trigger("a"));

        var drained = outbox.Drain();

        Assert.Same(first, Assert.Single(drained));
    }

    [Fact]
    public void Drain_DifferentKeys_KeepsAllInCollectionOrder()
    {
        var outbox = new IntegrationEventOutbox();
        var a = new Trigger("a");
        var b = new Trigger("b");
        var aAgain = new Trigger("a");
        outbox.Collect(a);
        outbox.Collect(b);
        outbox.Collect(aAgain);

        var drained = outbox.Drain();

        Assert.Equal(new IntegrationEvent[] { a, b }, drained);
    }

    [Fact]
    public void Drain_SameKeyOfDifferentTypes_KeepsBoth()
    {
        var outbox = new IntegrationEventOutbox();
        outbox.Collect(new Trigger("a"));
        outbox.Collect(new OtherTrigger("a"));

        var drained = outbox.Drain();

        Assert.Equal(2, drained.Count);
    }

    [Fact]
    public void Drain_NullKey_NeverCoalesces()
    {
        var outbox = new IntegrationEventOutbox();
        outbox.Collect(new Trigger(null));
        outbox.Collect(new Trigger(null));

        var drained = outbox.Drain();

        Assert.Equal(2, drained.Count);
    }

    [Fact]
    public void Drain_EventWithoutTheInterface_NeverCoalesces()
    {
        var outbox = new IntegrationEventOutbox();
        outbox.Collect(new Plain("a"));
        outbox.Collect(new Plain("a"));

        var drained = outbox.Drain();

        Assert.Equal(2, drained.Count);
    }

    [Fact]
    public void Drain_SameKeyInALaterDrain_IsKept()
    {
        var outbox = new IntegrationEventOutbox();
        outbox.Collect(new Trigger("a"));
        outbox.Drain();
        var later = new Trigger("a");
        outbox.Collect(later);

        var drained = outbox.Drain();

        Assert.Same(later, Assert.Single(drained));
    }

    private sealed record Trigger(string? Key) : IntegrationEvent, ICoalescableIntegrationEvent
    {
        string? ICoalescableIntegrationEvent.CoalesceKey => Key;
    }

    private sealed record OtherTrigger(string? Key) : IntegrationEvent, ICoalescableIntegrationEvent
    {
        string? ICoalescableIntegrationEvent.CoalesceKey => Key;
    }

    private sealed record Plain(string Data) : IntegrationEvent;
}
