namespace Outbox;

public class DlqMessage<TOriginalValue>
{
    public required DateTimeOffset FailedTimestampUtc { get; set; }
    public required string OriginalTopic { get; set; }
    public required int OriginalPartition { get; set; }
    public required long OriginalOffset { get; set; }
    public required string ConsumerGroupId { get; set; }
    public string? ExceptionType { get; set; }
    public string? ExceptionMessage { get; set; }
    public string? ExceptionStackTrace { get; set; }
    public TOriginalValue? OriginalMessage { get; set; }
}
