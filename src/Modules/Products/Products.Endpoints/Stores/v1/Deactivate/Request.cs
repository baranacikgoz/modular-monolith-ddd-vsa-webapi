using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.Deactivate;

public sealed record Request
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<StoreId>>]
    public required StoreId Id { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer.Stores_Deactivate_Id_NotEmpty);
    }
}
