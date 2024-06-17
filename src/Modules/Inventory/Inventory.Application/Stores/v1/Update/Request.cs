using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Inventory.Application.Stores.v1.Update;

public sealed record Request(string Name, string Description);

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage(localizer["v1.Update.Name.NotEmpty"])
            .MaximumLength(Domain.Stores.Constants.NameMaxLength)
                .WithMessage(localizer["v1.Update.Name.MaxLength", Domain.Stores.Constants.NameMaxLength]);

        RuleFor(x => x.Description)
            .MaximumLength(Domain.Stores.Constants.DescriptionMaxLength)
                .WithMessage(localizer["v1.Update.Description.MaxLength", Domain.Stores.Constants.DescriptionMaxLength]);
    }
}
