using System.ComponentModel.DataAnnotations;

namespace Common.Options;

public class CustomLocalizationOptions
{
    [Required(AllowEmptyStrings = false)]
    public string ResourcesPath { get; set; } = default!;

    [Required(AllowEmptyStrings = false)]
    public string DefaultCulture { get; set; } = default!;

    [Required(AllowEmptyStrings = false)]
    public ICollection<string> SupportedCultures { get; } = new List<string>();
}
