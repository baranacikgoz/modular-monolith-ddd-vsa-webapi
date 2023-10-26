using System.ComponentModel.DataAnnotations;

namespace Common.Options;

public class LoggerOptions
{
    [Required(AllowEmptyStrings = false)]
    public string AppName { get; set; } = default!;

    [Required(AllowEmptyStrings = false)]
    public string SeqConnectionString { get; set; } = default!;

    public bool WriteToFile { get; set; }
    public bool WriteToConsole { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string MinimumLogLevel { get; set; } = default!;
}
