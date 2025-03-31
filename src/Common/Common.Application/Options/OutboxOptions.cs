using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class OutboxOptions
{
    public int BackgroundJobPeriodInMilliseconds { get; set; }
    public int MaxBackoffDelayInMilliseconds { get; set; }
    public int BatchSizePerExecution { get; set; }
    public int MaxFailCountBeforeSentToDeadLetter { get; set; }
}

public class OutboxOptionsValidator : CustomValidator<OutboxOptions>
{
    public OutboxOptionsValidator()
    {
        RuleFor(o => o.BackgroundJobPeriodInMilliseconds)
            .GreaterThan(0)
            .WithMessage("BackgroundJobPeriodInMilliseconds must be greater than 0.");

        RuleFor(o => o.MaxBackoffDelayInMilliseconds)
            .GreaterThan(0)
            .WithMessage("MaxBackoffDelayInMilliseconds must be greater than 0.");

        RuleFor(o => o.BatchSizePerExecution)
            .GreaterThan(0)
            .WithMessage("BatchSizePerExecution must be greater than 0.");

        RuleFor(o => o.MaxFailCountBeforeSentToDeadLetter)
            .GreaterThan(0)
            .WithMessage("MaxFailCountBeforeSentToDeadLetter must be greater than 0.");
    }
}
