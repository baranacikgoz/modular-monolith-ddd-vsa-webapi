using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class HealthCheckOptions
{
    public bool EnableHealthChecks { get; set; } = true;
    public int LivenessTimeoutInSeconds { get; set; } = 3;
    public int ReadinessTimeoutInSeconds { get; set; } = 5;
    public int StartupTimeoutInSeconds { get; set; } = 10;
}

public class HealthCheckOptionsValidator : CustomValidator<HealthCheckOptions>
{
    public HealthCheckOptionsValidator()
    {
        RuleFor(o => o.LivenessTimeoutInSeconds)
            .GreaterThan(0)
            .WithMessage("LivenessTimeoutInSeconds must be greater than 0.");

        RuleFor(o => o.ReadinessTimeoutInSeconds)
            .GreaterThan(0)
            .WithMessage("ReadinessTimeoutInSeconds must be greater than 0.");

        RuleFor(o => o.StartupTimeoutInSeconds)
            .GreaterThan(0)
            .WithMessage("StartupTimeoutInSeconds must be greater than 0.");
    }
}
