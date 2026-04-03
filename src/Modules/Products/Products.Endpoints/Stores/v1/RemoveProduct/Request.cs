using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Products;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.RemoveProduct;

public sealed record Request
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<StoreId>>]
    public required StoreId Id { get; init; }

    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<ProductId>>]
    public required ProductId ProductId { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer.Stores_RemoveProduct_StoreId_NotEmpty);

        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage(localizer.Stores_RemoveProduct_ProductId_NotEmpty);
    }
}
