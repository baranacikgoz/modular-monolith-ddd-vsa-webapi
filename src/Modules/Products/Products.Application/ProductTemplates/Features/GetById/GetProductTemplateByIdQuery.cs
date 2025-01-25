using Common.Application.CQS;
using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Products.Application.ProductTemplates.DTOs;
using Products.Domain.ProductTemplates;

namespace Products.Application.ProductTemplates.Features.GetById;

public sealed record GetProductTemplateByIdQuery(ProductTemplateId Id) : IQuery<ProductTemplateDto>;

public sealed class GetProductTemplateByIdQueryValidator : CustomValidator<GetProductTemplateByIdQuery>
{
    public GetProductTemplateByIdQueryValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithMessage(localizer["ProductTemplates.GetById.Id.NotEmpty"]);
    }
}
