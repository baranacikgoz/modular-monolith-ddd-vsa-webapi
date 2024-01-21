using System.ComponentModel.DataAnnotations;

namespace Common.Options;

public class OpenApiOptions
{
    [Required(AllowEmptyStrings = false)]
    public string Title { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Description { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string ContactName { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string ContactEmail { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string LicenseName { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public Uri LicenseUrl { get; set; } = null!;
}
