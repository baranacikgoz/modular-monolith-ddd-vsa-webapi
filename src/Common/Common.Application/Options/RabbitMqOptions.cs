using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class RabbitMqOptions
{
    public required string Host { get; set; }
    public int Port { get; set; } = 5672;
    public string VirtualHost { get; set; } = "/";
    public required string Username { get; set; }
    public required string Password { get; set; }
}

public class RabbitMqOptionsValidator : CustomValidator<RabbitMqOptions>
{
    public RabbitMqOptionsValidator()
    {
        RuleFor(o => o.Host)
            .NotEmpty()
            .WithMessage("Host must not be empty.");

        RuleFor(o => o.Port)
            .InclusiveBetween(1, 65535)
            .WithMessage("Port must be between 1 and 65535.");

        RuleFor(o => o.VirtualHost)
            .NotEmpty()
            .WithMessage("VirtualHost must not be empty.");

        RuleFor(o => o.Username)
            .NotEmpty()
            .WithMessage("Username must not be empty.");

        RuleFor(o => o.Password)
            .NotEmpty()
            .WithMessage("Password must not be empty.");
    }
}
