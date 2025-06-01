using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;
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
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.Brand)
            .NotEmpty()
            .WithMessage(localizer["ProductTemplates.Create.Brand.NotEmpty"])
            .MaximumLength(Constants.BrandMaxLength)
            .WithMessage(localizer["ProductTemplates.Create.Brand.MaxLength {0}", Constants.BrandMaxLength]);

        RuleFor(x => x.Model)
            .NotEmpty()
            .WithMessage(localizer["ProductTemplates.Create.Model.NotEmpty"])
            .MaximumLength(Constants.ModelMaxLength)
            .WithMessage(localizer["ProductTemplates.Create.Model.MaxLength {0}", Constants.ModelMaxLength]);

        RuleFor(x => x.Color)
            .NotEmpty()
            .WithMessage(localizer["ProductTemplates.Create.Color.NotEmpty"])
            .MaximumLength(Constants.ColorMaxLength)
            .WithMessage(localizer["ProductTemplates.Create.Color.MaxLength {0}", Constants.ColorMaxLength]);
    }
}
