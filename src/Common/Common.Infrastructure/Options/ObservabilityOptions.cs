using System.ComponentModel.DataAnnotations;

namespace Common.Infrastructure.Options;

public class ObservabilityOptions
{
    [Required(AllowEmptyStrings = false)]
    public string AppName { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string AppVersion { get; set; } = null!;

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
    public string OtlpLoggingEndpoint { get; set; } = null!;

    [AllowedValues("HttpProtobuf", "Grpc")]
    public string OtlpLoggingProtocol { get; set; } = null!;

    [Required]
    public bool EnableMetrics { get; set; }
    public bool OtlpMetricsUsePrometheusDirectly { get; set; }
    public string? OtlpMetricsEndpoint { get; set; } = null!;

    [AllowedValues("HttpProtobuf", "Grpc")]
    public string? OtlpMetricsProtocol { get; set; } = null!;

    [Required]
    public bool EnableTracing { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string OtlpTracingEndpoint { get; set; } = null!;

    [AllowedValues("HttpProtobuf", "Grpc")]
    public string OtlpTracingProtocol { get; set; } = null!;
}
