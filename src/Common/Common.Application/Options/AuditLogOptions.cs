using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class AuditLogOptions
{
    public required int RetentionDays { get; set; }
}

public class AuditLogOptionsValidator : CustomValidator<AuditLogOptions>
{
    public AuditLogOptionsValidator()
    {
        RuleFor(x => x.RetentionDays)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Retention days must be at least 1.");
    }
}
