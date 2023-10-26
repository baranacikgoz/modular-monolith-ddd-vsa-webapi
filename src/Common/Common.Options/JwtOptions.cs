using System.ComponentModel.DataAnnotations;

namespace Common.Options;

public class JwtOptions
{
    [Required(AllowEmptyStrings = false)]
    public string Secret { get; set; } = default!;

    [Required(AllowEmptyStrings = false)]
    public string Issuer { get; set; } = default!;

    [Required(AllowEmptyStrings = false)]
    public string Audience { get; set; } = default!;

    [Required]
    public int AccessTokenExpirationInMinutes { get; set; }

    [Required]
    public int RefreshTokenExpirationInDays { get; set; }
}
