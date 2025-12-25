using Common.Application.Localization;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.Get;

public sealed record Request
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<StoreId>>]
    public StoreId Id { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer["Stores.GetById.Id.NotEmpty"]);
    }
}
