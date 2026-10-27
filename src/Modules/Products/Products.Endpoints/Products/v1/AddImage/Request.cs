using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Products;

namespace Products.Endpoints.Products.v1.AddImage;

public sealed record Request
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<ProductId>>]
    public ProductId Id { get; init; }

    /// <summary>A JPEG, PNG or WebP image. The type is decided from the file's first bytes, never from its name or the
    /// client's content type.</summary>
    [FromForm]
    public required IFormFile File { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer.Products_AddImage_Id_NotEmpty);

        RuleFor(x => x.File)
            .NotNull()
            .WithMessage(localizer.Products_AddImage_File_NotEmpty)
            .Must(file => file.Length > 0)
            .WithMessage(localizer.Products_AddImage_File_NotEmpty);
    }
}
