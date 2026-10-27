using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Products;

namespace Products.Endpoints.Products.v1.RemoveImage;

public sealed record Request
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<ProductId>>]
    public ProductId Id { get; init; }

    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<ProductImageId>>]
    public ProductImageId ImageId { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer.Products_RemoveImage_Id_NotEmpty);

        RuleFor(x => x.ImageId)
            .NotEmpty()
            .WithMessage(localizer.Products_RemoveImage_ImageId_NotEmpty);
    }
}
