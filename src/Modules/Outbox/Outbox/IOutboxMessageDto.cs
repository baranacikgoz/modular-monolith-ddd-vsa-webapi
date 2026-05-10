namespace Outbox;

public interface IOutboxMessageDto
{
    int Id { get; }
    bool IsProcessed { get; }
}
