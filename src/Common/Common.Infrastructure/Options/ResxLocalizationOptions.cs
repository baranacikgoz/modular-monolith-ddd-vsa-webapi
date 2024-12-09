using Common.Application.Validation;
using FluentValidation;

namespace Common.Infrastructure.Options;

public class ResxLocalizationOptions
{
    public required string DefaultCulture { get; set; }

    public ICollection<string> SupportedCultures { get; } = [];
}

public class ResxLocalizationOptionsValidator : CustomValidator<ResxLocalizationOptions>
{
    public ResxLocalizationOptionsValidator()
    {
        RuleFor(o => o.DefaultCulture)
            .NotEmpty()
            .WithMessage("DefaultCulture must not be empty.");

        RuleFor(o => o.SupportedCultures)
            .NotNull()
            .WithMessage("SupportedCultures must not be null.")
            .NotEmpty()
            .WithMessage("SupportedCultures must contain at least one culture.");
    }
}
