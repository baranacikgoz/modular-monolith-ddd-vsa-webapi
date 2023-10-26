using System.ComponentModel.DataAnnotations;

namespace Common.Options;

public class DatabaseOptions
{
    [Required(AllowEmptyStrings = false)]
    public string ConnectionString { get; set; } = default!;

}
