using Common.Application.Localization;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Products.Domain.Products;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.RemoveProduct;

public sealed record Request
{
    [FromRoute, ModelBinder<StronglyTypedIdBinder<StoreId>>]
    public required StoreId Id { get; init; }

    [FromRoute, ModelBinder<StronglyTypedIdBinder<ProductId>>]
    public required ProductId ProductId { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer["Stores.RemoveProduct.StoreId.NotEmpty"]);

        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage(localizer["Stores.RemoveProduct.ProductId.NotEmpty"]);
    }
}

