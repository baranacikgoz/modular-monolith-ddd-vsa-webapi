using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

/// <summary>Bounds for <c>ProjectionReconciliationJob</c> runs.</summary>
public class ProjectionReconciliationOptions
{
    /// <summary>Source items fetched and applied per page (one SaveChanges each).</summary>
    public required int PageSize { get; set; }

    /// <summary>Pages one run may process before it stops and leaves the rest for the next run.</summary>
    public required int MaxPages { get; set; }
}

public class ProjectionReconciliationOptionsValidator : CustomValidator<ProjectionReconciliationOptions>
{
    public ProjectionReconciliationOptionsValidator()
    {
        RuleFor(o => o.PageSize)
            .GreaterThan(0)
            .WithMessage("PageSize must be greater than 0.");

        RuleFor(o => o.MaxPages)
            .GreaterThan(0)
            .WithMessage("MaxPages must be greater than 0.");
    }
}
