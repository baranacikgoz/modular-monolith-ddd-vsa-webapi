using Common.Application.CQS;
using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Products.Domain.Products;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;

namespace Products.Application.Stores.Features.AddProduct;

public sealed record AddProductCommand(StoreId StoreId, ProductTemplateId ProductTemplateId, string Name, string Description, int Quantity, decimal Price) : ICommand<ProductId>;

public sealed class AddProductCommandValidator : CustomValidator<AddProductCommand>
{
    public AddProductCommandValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.StoreId)
            .NotEmpty()
                .WithMessage(localizer["Stores.AddProduct.StoreId.NotEmpty"]);

        RuleFor(x => x.ProductTemplateId)
            .NotEmpty()
                .WithMessage(localizer["Stores.AddProduct.ProductTemplateId.NotEmpty"]);

        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage(localizer["Stores.AddProduct.Name.NotEmpty"])
            .MaximumLength(Domain.Products.Constants.NameMaxLength)
                .WithMessage(localizer["Stores.AddProduct.Name.MaxLength {0}", Domain.Products.Constants.NameMaxLength]);

        RuleFor(x => x.Description)
            .NotEmpty()
                .WithMessage(localizer["Stores.AddProduct.Description.NotEmpty"])
            .MaximumLength(Domain.Products.Constants.DescriptionMaxLength)
                .WithMessage(localizer["Stores.AddProduct.Description.MaxLength {0}", Domain.Products.Constants.DescriptionMaxLength]);

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(Domain.Products.Constants.QuantityGreaterThanOrEqualTo)
                .WithMessage(localizer["Stores.AddProduct.Quantity.GreaterThanOrEqualTo {0}", Domain.Products.Constants.QuantityGreaterThanOrEqualTo]);

        RuleFor(x => x.Price)
            .GreaterThan(Domain.Products.Constants.PriceGreaterThanOrEqualTo)
                .WithMessage(localizer["Stores.AddProduct.Price.GreaterThanOrEqualTo {0}", Domain.Products.Constants.PriceGreaterThanOrEqualTo]);
    }
}
