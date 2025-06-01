using Common.Application.Localization;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Products.Domain.Products;

namespace Products.Endpoints.Stores.v1.My.RemoveProduct;

public sealed record Request
{
    [FromRoute, ModelBinder<StronglyTypedIdBinder<ProductId>>]
    public ProductId Id { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer["Stores.My.RemoveProduct.Id.NotEmpty"]);
    }
}
