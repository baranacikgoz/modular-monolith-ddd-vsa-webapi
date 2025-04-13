namespace Outbox;

public class DlqMessage<TOriginalValue>
{
    public DateTimeOffset FailedTimestampUtc { get; set; }
    public string OriginalTopic { get; set; } = string.Empty;
    public int OriginalPartition { get; set; }
    public long OriginalOffset { get; set; }
    public string ConsumerGroupId { get; set; } = string.Empty;
    public string? ExceptionType { get; set; }
    public string? ExceptionMessage { get; set; }
    public string? ExceptionStackTrace { get; set; }
    public TOriginalValue? OriginalMessage { get; set; } // The full original message value
}
