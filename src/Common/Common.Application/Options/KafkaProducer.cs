using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class KafkaProducer
{
    public required string BootstrapServers { get; set; }
    public required string TopicName { get; set; }
}

public class KafkaProducerValidator : CustomValidator<KafkaProducer>
{
    public KafkaProducerValidator()
    {
        RuleFor(x => x.BootstrapServers)
            .NotEmpty()
            .WithMessage("BootstrapServers is required");

        RuleFor(x => x.TopicName)
            .NotEmpty()
            .WithMessage("TopicName is required");
    }
}
