using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

/// <summary>Thresholds for <c>JobHousekeepingJob</c> (stale Running rows, retention of finished rows).</summary>
public class JobHousekeepingOptions
{
    /// <summary>A Running row started longer ago than this is failed as stale.</summary>
    public required int StaleAfterMinutes { get; set; }

    /// <summary>Succeeded/Failed rows finished longer ago than this are deleted.</summary>
    public required int RetentionHours { get; set; }

    /// <summary>Rows deleted per statement.</summary>
    public required int PageSize { get; set; }
}

public class JobHousekeepingOptionsValidator : CustomValidator<JobHousekeepingOptions>
{
    public JobHousekeepingOptionsValidator()
    {
        RuleFor(o => o.StaleAfterMinutes)
            .GreaterThan(0)
            .WithMessage("StaleAfterMinutes must be greater than 0.");

        RuleFor(o => o.RetentionHours)
            .GreaterThan(0)
            .WithMessage("RetentionHours must be greater than 0.");

        RuleFor(o => o.PageSize)
            .GreaterThan(0)
            .WithMessage("PageSize must be greater than 0.");
    }
}
