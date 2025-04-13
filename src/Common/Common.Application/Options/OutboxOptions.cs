using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class OutboxOptions
{
    public required KafkaConsumer KafkaConsumer { get; set; }
    public required KafkaProducer KafkaDlqProducer { get; set; }
    public required int SetupRetryDelaySeconds { get; set; }
    public required int ConsumeErrorDelaySeconds { get; set; }
    public required int ProcessingErrorDelaySeconds { get; set; }
}

public class OutboxOptionsValidator : CustomValidator<OutboxOptions>
{
    public OutboxOptionsValidator()
    {
        RuleFor(o => o.KafkaConsumer)
            .SetValidator(new KafkaConsumerValidator());

        RuleFor(o => o.KafkaDlqProducer)
            .SetValidator(new KafkaProducerValidator());

        RuleFor(o => o.SetupRetryDelaySeconds)
            .GreaterThanOrEqualTo(1)
            .WithMessage("SetupRetryDelaySeconds must be at least 1.");

        RuleFor(o => o.ConsumeErrorDelaySeconds)
            .GreaterThanOrEqualTo(1)
            .WithMessage("ConsumeErrorDelaySeconds must be at least 1.");

        RuleFor(o => o.ProcessingErrorDelaySeconds)
            .GreaterThanOrEqualTo(0) // Allow 0 for immediate retry, though >=1 is often better
            .WithMessage("ProcessingErrorDelaySeconds cannot be negative.");
    }
}
