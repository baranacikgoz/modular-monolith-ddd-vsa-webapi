using Common.Application.Localization;
using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Products.Domain.ProductTemplates;

namespace Products.Endpoints.ProductTemplates.v1.Get;

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
            .WithMessage(localizer.ProductTemplates_Get_Id_NotEmpty);
    }
}
