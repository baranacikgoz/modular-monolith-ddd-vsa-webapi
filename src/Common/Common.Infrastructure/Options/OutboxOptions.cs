using Common.Application.Validation;
using FluentValidation;

namespace Common.Infrastructure.Options;

public class OutboxOptions
{
    public int BackgroundJobPeriodInSeconds { get; set; }

    public int BatchSizePerExecution { get; set; }

    public int MaxFailCountBeforeSentToDeadLetter { get; set; }
}

public class OutboxOptionsValidator : CustomValidator<OutboxOptions>
{
    public OutboxOptionsValidator()
    {
        RuleFor(o => o.BackgroundJobPeriodInSeconds)
            .GreaterThan(0)
            .WithMessage("BackgroundJobPeriodInSeconds must be greater than 0.");

        RuleFor(o => o.BatchSizePerExecution)
            .GreaterThan(0)
            .WithMessage("BatchSizePerExecution must be greater than 0.");

        RuleFor(o => o.MaxFailCountBeforeSentToDeadLetter)
            .GreaterThan(0)
            .WithMessage("MaxFailCountBeforeSentToDeadLetter must be greater than 0.");
    }
}
