using System.ComponentModel.DataAnnotations;

namespace Common.Options;

public class MessageBrokerOptions
{
    [Required(AllowEmptyStrings = false)]
    public Uri Uri { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Username { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Password { get; set; } = null!;
}
