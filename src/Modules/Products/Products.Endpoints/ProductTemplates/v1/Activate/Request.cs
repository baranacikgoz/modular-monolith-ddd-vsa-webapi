using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.ProductTemplates;

namespace Products.Endpoints.ProductTemplates.v1.Activate;

public sealed record Request
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<ProductTemplateId>>]
    public required ProductTemplateId Id { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer.ProductTemplates_Activate_Id_NotEmpty);
    }
}
