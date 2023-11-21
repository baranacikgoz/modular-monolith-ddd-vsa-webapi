using System.ComponentModel.DataAnnotations;

namespace Common.Options;

public class CustomLoggingOptions
{
    [Required]
    public int ResponseTimeThresholdInMs { get; set; }

    [Required(AllowEmptyStrings = false)]
    public Uri SeqUrl { get; set; } = null!;
}
