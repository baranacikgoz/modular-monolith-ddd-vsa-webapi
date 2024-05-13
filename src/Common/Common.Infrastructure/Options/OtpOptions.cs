using System.ComponentModel.DataAnnotations;

namespace Common.Infrastructure.Options;

public class OtpOptions
{
    [Required]
    public int ExpirationInMinutes { get; set; }
}
