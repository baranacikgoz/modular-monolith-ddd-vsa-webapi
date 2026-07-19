using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class InterModuleRequestOptions
{
    // Sync request/response inside HTTP request paths — fail fast rather than pinning
    // the caller for MassTransit's default 30s when the target module is down.
    public required int TimeoutSeconds { get; set; }
}

public class InterModuleRequestOptionsValidator : CustomValidator<InterModuleRequestOptions>
{
    public InterModuleRequestOptionsValidator()
    {
        RuleFor(o => o.TimeoutSeconds)
            .GreaterThan(0)
            .WithMessage("TimeoutSeconds must be greater than 0.");
    }
}
