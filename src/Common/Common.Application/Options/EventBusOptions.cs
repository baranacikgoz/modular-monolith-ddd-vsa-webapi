using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class EventBusOptions
{
    public bool UseInMemoryEventBus { get; set; }
    public MessageBroker? MessageBroker { get; set; }
}

public class MessageBroker
{
    public MessageBrokerType MessageBrokerType { get; set; }

    public required string Uri { get; set; }

    public required string Username { get; set; }

    public required string Password { get; set; }
}

public enum MessageBrokerType
{
    RabbitMQ,
    Kafka,
    AzureServiceBus,
    AmazonSQS
}

public class EventBusOptionsValidator : CustomValidator<EventBusOptions>
{
    public EventBusOptionsValidator()
    {
        RuleFor(o => o)
            .Must(o => o.UseInMemoryEventBus || o.MessageBroker is not null)
            .WithMessage("Either UseInMemoryEventBus must be true or MessageBrokerOptions must not be null.");
    }
}

public class MessageBrokerOptionsValidator : CustomValidator<MessageBroker>
{
    public MessageBrokerOptionsValidator()
    {
        RuleFor(o => o.MessageBrokerType)
            .IsInEnum()
            .WithMessage("MessageBrokerType must be a valid enum value.");

        RuleFor(o => o.Uri)
            .NotEmpty()
            .WithMessage("Uri must not be empty.")
            .Must(x => Uri.TryCreate(x, UriKind.Absolute, out _))
            .WithMessage("Uri must be a valid URL.");

        RuleFor(o => o.Username)
            .NotEmpty()
            .WithMessage("Username must not be empty.");

        RuleFor(o => o.Password)
            .NotEmpty()
            .WithMessage("Password must not be empty.");
    }
}
