using System.ComponentModel.DataAnnotations;

namespace Common.Options;

public class RabbitMqOptions
{
    [Required(AllowEmptyStrings = false)]
    public string Host { get; set; } = default!;

    [Required(AllowEmptyStrings = false)]
    public string Username { get; set; } = default!;

    [Required(AllowEmptyStrings = false)]
    public string Password { get; set; } = default!;

    public int Port { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string IdentityAndAuthModuleQueueName { get; set; } = default!;

    [Required(AllowEmptyStrings = false)]
    public string AppointmentsModuleQueueName { get; set; } = default!;
}
