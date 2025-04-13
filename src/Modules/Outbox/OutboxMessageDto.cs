using Common.Domain.StronglyTypedIds;

namespace Outbox;

public class OutboxMessageDto
{
    public int Id { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public bool IsProcessed { get; set; }
    public DateTimeOffset? ProcessedOn { get; set; }
    public required string Event { get; set; }
}
