using System.ComponentModel.DataAnnotations;

namespace Common.Options;

public class OutboxOptions
{
    [Required]
    public int BackgroundJobPeriodInSeconds { get; set; }

    [Required]
    public int BatchSizePerExecution { get; set; }

    [Required]
    public int MaxFailCountBeforeSentToDeadLetter { get; set; }
}
