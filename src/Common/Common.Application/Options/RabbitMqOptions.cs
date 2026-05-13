namespace Common.Application.Options;

public class RabbitMqOptions
{
    public required string Host { get; set; }
    public int Port { get; set; } = 5672;
    public string VirtualHost { get; set; } = "/";
    public required string Username { get; set; }
    public required string Password { get; set; }
}
