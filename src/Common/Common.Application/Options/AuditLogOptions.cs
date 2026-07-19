using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class AuditLogOptions
{
    public required int RetentionDays { get; set; }
    public int PurgeBatchSize { get; set; } = 5000;
}

public class AuditLogOptionsValidator : CustomValidator<AuditLogOptions>
{
    public AuditLogOptionsValidator()
    {
        RuleFor(x => x.RetentionDays)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Retention days must be at least 1.");

        RuleFor(x => x.PurgeBatchSize)
            .GreaterThanOrEqualTo(100)
            .WithMessage("Purge batch size must be at least 100.");
    }
}
