using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class HealthCheckOptions
{
    public bool EnableHealthChecks { get; set; }
    public required int ReadinessTimeoutInSeconds { get; set; }
    public required int StartupTimeoutInSeconds { get; set; }
}

public class HealthCheckOptionsValidator : CustomValidator<HealthCheckOptions>
{
    public HealthCheckOptionsValidator()
    {
        RuleFor(o => o.ReadinessTimeoutInSeconds)
            .GreaterThan(0)
            .WithMessage("ReadinessTimeoutInSeconds must be greater than 0.");

        RuleFor(o => o.StartupTimeoutInSeconds)
            .GreaterThan(0)
            .WithMessage("StartupTimeoutInSeconds must be greater than 0.");
    }
}
