using Common.Application.Validation;
using FluentValidation;

namespace Common.Infrastructure.Options;

public class OpenApiOptions
{
    public bool EnableSwagger { get; set; }
    public required string Title { get; set; }

    public required string Description { get; set; }

    public required string ContactName { get; set; }

    public required string ContactEmail { get; set; }

    public required string LicenseName { get; set; }

    public required string LicenseUrl { get; set; }
}

public class OpenApiOptionsValidator : CustomValidator<OpenApiOptions>
{
    public OpenApiOptionsValidator()
    {
        RuleFor(o => o.Title)
            .NotEmpty()
            .WithMessage("Title must not be empty.");

        RuleFor(o => o.Description)
            .NotEmpty()
            .WithMessage("Description must not be empty.");

        RuleFor(o => o.ContactName)
            .NotEmpty()
            .WithMessage("ContactName must not be empty.");

        RuleFor(o => o.ContactEmail)
            .NotEmpty()
            .WithMessage("ContactEmail must not be empty.");

        RuleFor(o => o.LicenseName)
            .NotEmpty()
            .WithMessage("LicenseName must not be empty.");

        RuleFor(o => o.LicenseUrl)
            .NotEmpty()
            .WithMessage("LicenseUrl must not be empty.")
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
            .WithMessage("LicenseUrl must be a valid URL.");
    }
}
