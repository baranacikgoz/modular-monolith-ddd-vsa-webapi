using Common.Domain.Events;

namespace Common.Infrastructure.Persistence.Outbox;

public class DeadLetterMessage : OutboxMessageBase
{
    private DeadLetterMessage(DomainEvent @event, int failedCount, DateTimeOffset? lastFailedOn)
        : base(@event)
    {
        FailedCount = failedCount;
        LastFailedOn = lastFailedOn;
    }

    public static DeadLetterMessage CreateFrom(OutboxMessage outboxMessage)
        => new(outboxMessage.Event, outboxMessage.FailedCount, outboxMessage.LastFailedOn);

    // Required for deserialization
    public DeadLetterMessage() : base(null!)
    {}
}
