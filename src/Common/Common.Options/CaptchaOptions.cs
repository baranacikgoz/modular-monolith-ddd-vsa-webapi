using System.ComponentModel.DataAnnotations;

namespace Common.Options;

#pragma warning disable CA1056
public class CaptchaOptions
{
    [Required(AllowEmptyStrings = false), Url]
    public string BaseUrl { get; set; } = default!;

    [Required(AllowEmptyStrings = false)]
    public string CaptchaEndpoint { get; set; } = default!;

    [Required(AllowEmptyStrings = false)]
    public string ClientKey { get; set; } = default!;

    [Required(AllowEmptyStrings = false)]
    public string SecretKey { get; set; } = default!;
}
#pragma warning restore CA1056
