using System.ComponentModel.DataAnnotations;

namespace Common.Options;

public class CustomRateLimitingOptions
{

    [Required]
    public FixedWindow? Global { get; set; } = default!;

    [Required]
    public FixedWindow? Sms { get; set; } = default!;

    [Required]
    public FixedWindow? SearchAppointments { get; set; } = default!;
}

public class FixedWindow
{
    [Required]
    public int Limit { get; set; }

    [Required]
    public int PeriodInMs { get; set; }

    public int? QueueLimit { get; set; }
    public bool HasQueueLimit => QueueLimit is not null && QueueLimit > 0;
}
