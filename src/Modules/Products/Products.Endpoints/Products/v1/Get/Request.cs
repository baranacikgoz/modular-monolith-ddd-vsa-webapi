using Common.Application.Localization;
using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Products.Domain.Products;

namespace Products.Endpoints.Products.v1.Get;

public sealed record Request
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<ProductId>>]
    public ProductId Id { get; set; }
}

public class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer.Products_v1_Get_Id_NotEmpty);
    }
}
