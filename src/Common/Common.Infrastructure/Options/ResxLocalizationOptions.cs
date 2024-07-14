using System.ComponentModel.DataAnnotations;

namespace Common.Infrastructure.Options;

public class ResxLocalizationOptions
{
    [Required(AllowEmptyStrings = false)]
    public string DefaultCulture { get; set; } = default!;

    [Required(AllowEmptyStrings = false)]
    public ICollection<string> SupportedCultures { get; } = [];
}
