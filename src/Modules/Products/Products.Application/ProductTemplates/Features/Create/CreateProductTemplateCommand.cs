using Common.Application.CQS;
using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Products.Domain.ProductTemplates;

namespace Products.Application.ProductTemplates.Features.Create;

public sealed record CreateProductTemplateCommand(string Brand, string Model, string Color) : ICommand<ProductTemplateId>;

public sealed class CreateProductCommandValidator : CustomValidator<CreateProductTemplateCommand>
{
    public CreateProductCommandValidator(IStringLocalizer<ResxLocalizer> localizer)
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
