using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Stores;

namespace Products.Endpoints.Products.v1.Export;

public sealed record Request
{
    /// <summary>Only the products of this store; every product when absent.</summary>
    [FromQuery]
    [ModelBinder<StronglyTypedIdBinder<StoreId>>]
    public StoreId? StoreId { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.StoreId)
            .NotEmpty()
            .WithMessage(localizer.Products_Export_StoreId_NotEmpty)
            .When(x => x.StoreId is not null);
    }
}
