using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.Validation;
using FluentValidation;
using Products.Domain.ProductTemplates;

namespace Products.Endpoints.ProductTemplates.v1.Create;

public sealed record Request
{
    public required string Brand { get; init; }
    public required string Model { get; init; }
    public required string Color { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Brand)
            .NotEmpty()
            .WithMessage(localizer.ProductTemplates_Create_Brand_NotEmpty)
            .MaximumLength(Constants.BrandMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.ProductTemplates_Create_Brand_MaxLength,
                Constants.BrandMaxLength));

        RuleFor(x => x.Model)
            .NotEmpty()
            .WithMessage(localizer.ProductTemplates_Create_Model_NotEmpty)
            .MaximumLength(Constants.ModelMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.ProductTemplates_Create_Model_MaxLength,
                Constants.ModelMaxLength));

        RuleFor(x => x.Color)
            .NotEmpty()
            .WithMessage(localizer.ProductTemplates_Create_Color_NotEmpty)
            .MaximumLength(Constants.ColorMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.ProductTemplates_Create_Color_MaxLength,
                Constants.ColorMaxLength));
    }
}
