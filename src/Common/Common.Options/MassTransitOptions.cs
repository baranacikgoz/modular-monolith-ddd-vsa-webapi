using System.ComponentModel.DataAnnotations;

namespace Common.Options;

public class MassTransitOptions
{
    [Required]
    public int DuplicateDetectionWindowInSeconds { get; set; }

}
