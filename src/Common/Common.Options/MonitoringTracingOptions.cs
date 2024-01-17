using System.ComponentModel.DataAnnotations;

namespace Common.Options;

public class MonitoringTracingOptions
{
    [Required(AllowEmptyStrings = false)]
    public string ServiceName { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string TracingExporter { get; set; } = null!;

    public string OtlpEndpoint { get; set; } = null!;
    public string ZipkinEndpoint { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string MetricsExporter { get; set; } = null!;
}
