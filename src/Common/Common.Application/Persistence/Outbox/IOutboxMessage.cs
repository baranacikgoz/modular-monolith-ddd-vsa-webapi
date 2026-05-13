using Common.Domain.Events;

namespace Common.Application.Persistence.Outbox;

public interface IOutboxMessage
{
    int Id { get; }
    DateTimeOffset CreatedOn { get; }
    IEvent? Event { get; }
    bool IsProcessed { get; }
    DateTimeOffset? ProcessedOn { get; }
    void MarkAsProcessed(DateTimeOffset processedOn);
}
