using Common.Domain.StronglyTypedIds;

namespace Outbox;

public class OutboxMessageDto
{
    public int Id { get; set; }
    public bool IsProcessed { get; set; }
    public DateTimeOffset? ProcessedOn { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public ApplicationUserId? CreatedBy { get; set; }
    public DateTimeOffset? LastModifiedOn { get; set; }
    public ApplicationUserId? LastModifiedBy { get; set; }
    public required string Event { get; set; }
    public int FailedCount { get; set; }
    public DateTimeOffset? LastFailedOn { get; set; }
}
