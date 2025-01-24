using Common.Application.CQS;
using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Products.Domain.Products;
using Products.Domain.Stores;

namespace Products.Application.Stores.Features.RemoveProduct;

public sealed record RemoveProductCommand(StoreId StoreId, ProductId ProductId) : ICommand;

public sealed class RemoveProductCommandValidator : CustomValidator<RemoveProductCommand>
{
    public RemoveProductCommandValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.StoreId)
            .NotEmpty()
                .WithMessage(localizer["Stores.RemoveProduct.StoreId.NotEmpty"]);

        RuleFor(x => x.ProductId)
            .NotEmpty()
                .WithMessage(localizer["Stores.RemoveProduct.ProductId.NotEmpty"]);
    }
}
