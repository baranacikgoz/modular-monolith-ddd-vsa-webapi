using System.ComponentModel.DataAnnotations;

namespace Common.Options;

public class MonitoringOptions
{
    [Required(AllowEmptyStrings = false)]
    public string OtlpEndpoint { get; set; } = null!;
    [Required(AllowEmptyStrings = false)]
    public string ServiceName { get; set; } = null!;
}
