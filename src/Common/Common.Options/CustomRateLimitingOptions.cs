using System.ComponentModel.DataAnnotations;

namespace Common.Options;

public class CustomRateLimitingOptions
{

    [Required]
    public FixedWindow? Global { get; set; } = default!;

    [Required]
    public FixedWindow? Sms { get; set; } = default!;

    [Required]
    public FixedWindow? CreateStore { get; set; } = default!;
}

public class FixedWindow
{
    [Required]
    public int Limit { get; set; }

    [Required]
    public double PeriodInMs { get; set; }

    [Required]
    public int QueueLimit { get; set; }
}
