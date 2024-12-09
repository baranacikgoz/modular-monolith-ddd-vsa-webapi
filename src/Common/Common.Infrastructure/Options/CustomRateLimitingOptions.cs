using Common.Application.Validation;
using FluentValidation;

namespace Common.Infrastructure.Options;

public class CustomRateLimitingOptions
{
    public required FixedWindow Global { get; set; }

    public required FixedWindow Sms { get; set; }

    public required FixedWindow CreateStore { get; set; }
}

public class FixedWindow
{
    public int Limit { get; set; }

    public double PeriodInMs { get; set; }

    public int QueueLimit { get; set; }
}

public class CustomRateLimitingOptionsValidator : CustomValidator<CustomRateLimitingOptions>
{
    public CustomRateLimitingOptionsValidator()
    {
#pragma warning disable CS8620
        RuleFor(o => o.Global)
            .SetValidator(new FixedWindowValidator());

        RuleFor(o => o.Sms)
            .SetValidator(new FixedWindowValidator());

        RuleFor(o => o.CreateStore)
            .SetValidator(new FixedWindowValidator());
#pragma warning restore CS8620
    }
}

public class FixedWindowValidator : CustomValidator<FixedWindow>
{
    public FixedWindowValidator()
    {
        RuleFor(o => o.Limit)
            .NotEmpty()
            .WithMessage("Limit must not be empty.");

        RuleFor(o => o.PeriodInMs)
            .NotEmpty()
            .WithMessage("PeriodInMs must not be empty.");

        RuleFor(o => o.QueueLimit)
            .GreaterThanOrEqualTo(0)
            .WithMessage("QueueLimit must be greater than or equal to 0.");
    }
}
