using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Products.Domain.Products;

namespace Products.Endpoints.Products.v1.My.Get;

public sealed record Request
{
    [FromRoute, ModelBinder<StronglyTypedIdBinder<ProductId>>]
    public ProductId Id { get; set; }
}

public class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer["Products.v1.My.Get.Id.Empty"]);
    }
}
