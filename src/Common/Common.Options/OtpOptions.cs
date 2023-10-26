using System.ComponentModel.DataAnnotations;

namespace Common.Options;

public class OtpOptions
{
    [Required]
    public int ExpirationInMinutes { get; set; }
}
