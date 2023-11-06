using System.ComponentModel.DataAnnotations;

namespace Common.Options;

public class CustomLoggingOptions
{
    [Required]
    public int ResponseTimeThresholdInMs { get; set; }
}
