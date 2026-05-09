namespace Outbox;

public class IntegrationEventOutboxMessageDto
{
    public required int Id { get; set; }
    public required DateTimeOffset CreatedOn { get; set; }
    public required bool IsProcessed { get; set; }
    public DateTimeOffset? ProcessedOn { get; set; }
    public required string Event { get; set; }
}
