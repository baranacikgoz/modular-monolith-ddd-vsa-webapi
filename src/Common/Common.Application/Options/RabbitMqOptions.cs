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

    // Transient consumer failures (DB blips, pool timeouts) retry with exponential backoff instead
    // of faulting straight to the _error queue. Consumers are idempotent (IntegrationEventHandlerBase),
    // so redelivery is safe.
    public required int RetryLimit { get; set; }
    public required int RetryMinIntervalMs { get; set; }
    public required int RetryMaxIntervalMs { get; set; }
    public required int RetryIntervalDeltaMs { get; set; }
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

        RuleFor(o => o.RetryLimit)
            .GreaterThanOrEqualTo(0)
            .WithMessage("RetryLimit must be at least 0.");

        RuleFor(o => o.RetryMinIntervalMs)
            .GreaterThan(0)
            .WithMessage("RetryMinIntervalMs must be greater than 0.");

        RuleFor(o => o.RetryMaxIntervalMs)
            .GreaterThanOrEqualTo(o => o.RetryMinIntervalMs)
            .WithMessage("RetryMaxIntervalMs must be >= RetryMinIntervalMs.");

        RuleFor(o => o.RetryIntervalDeltaMs)
            .GreaterThanOrEqualTo(0)
            .WithMessage("RetryIntervalDeltaMs must be at least 0.");
    }
}
