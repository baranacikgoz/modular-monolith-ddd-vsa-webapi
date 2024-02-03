using System.ComponentModel.DataAnnotations;

namespace Common.Options;

public class MessageBrokerOptions
{
    [Required(AllowEmptyStrings = false)]
    public string Host { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Port { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Username { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Password { get; set; } = null!;
}
