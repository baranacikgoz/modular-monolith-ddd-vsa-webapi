using System.ComponentModel.DataAnnotations;

namespace Common.Options;

public class LoggingMonitoringTracingOptions
{
    [Required(AllowEmptyStrings = false)]
    public string AppName { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string MinimumLevel { get; set; } = null!;

    [Required]
    public Dictionary<string, string> MinimumLevelOverrides { get; } = [];

    [Required]
    public bool WriteToConsole { get; set; }

    [Required]
    public bool WriteToFile { get; set; }

    [Required]
    public int ResponseTimeThresholdInMs { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string SeqUrl { get; set; } = null!;

    [Required]
    public bool EnableMetrics { get; set; }

    [Required]
    public bool EnableTracing { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string OtlpTracingEndpoint { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string OtlpMetricsEndpoint { get; set; } = null!;
}
