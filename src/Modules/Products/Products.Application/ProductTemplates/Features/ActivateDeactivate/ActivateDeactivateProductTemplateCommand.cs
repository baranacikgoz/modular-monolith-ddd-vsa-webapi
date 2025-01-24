using Common.Application.CQS;
using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Products.Domain.ProductTemplates;

namespace Products.Application.ProductTemplates.Features.ActivateDeactivate;

public sealed record ActivateDeactivateProductTemplateCommand(ProductTemplateId Id, bool Activate) : ICommand;

public sealed class ActivateDeactivateProductTemplateCommandValidator : CustomValidator<ActivateDeactivateProductTemplateCommand>
{
    public ActivateDeactivateProductTemplateCommandValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer["ProductTemplates.ActivateDeactivate.Id.NotEmpty"]);
    }
}
