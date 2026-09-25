using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class AuditLogOptions
{
    public required int RetentionDays { get; set; }
    public required int PurgeBatchSize { get; set; }

    /// <summary>Cron of the recurring purge job (Hangfire syntax, evaluated in the server's local time zone).</summary>
    public required string RetentionCron { get; set; }

    /// <summary>
    ///     Retention days per schema (module name), overriding <see cref="RetentionDays"/> for that schema only.
    /// </summary>
    public Dictionary<string, int> PerSchemaRetentionDays { get; init; } = [];
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

        RuleFor(x => x.RetentionCron)
            .NotEmpty()
            .WithMessage("RetentionCron must not be empty.");

        RuleForEach(x => x.PerSchemaRetentionDays)
            .Must(kvp => !string.IsNullOrWhiteSpace(kvp.Key) && kvp.Value >= 1)
            .WithMessage("Each PerSchemaRetentionDays entry must have a non-empty schema name and a value of at least 1.");
    }
}
