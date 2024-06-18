using Common.Application.Localization;
using Common.Application.Validation;
using Common.Domain.StronglyTypedIds;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Inventory.Application.Stores.v1.My.Create;

public sealed record Request(string Name, string Description);

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage(localizer["Stores.v1.Create.Name.NotEmpty"])
            .MaximumLength(Domain.Stores.Constants.NameMaxLength)
                .WithMessage(localizer["Stores.v1.Create.Name.MaxLength {0}", Domain.Stores.Constants.NameMaxLength]);

        RuleFor(x => x.Description)
            .NotEmpty()
                .WithMessage(localizer["Stores.v1.Create.Description.NotEmpty"])
            .MaximumLength(Domain.Stores.Constants.DescriptionMaxLength)
                .WithMessage(localizer["Stores.v1.Create.Description.MaxLength {0}", Domain.Stores.Constants.DescriptionMaxLength]);
    }
}
