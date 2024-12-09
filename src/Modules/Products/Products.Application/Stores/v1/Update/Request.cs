using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Products.Application.Stores.v1.Update;

public sealed record Request(string Name, string Description);

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage(localizer["Stores.v1.Update.Name.NotEmpty"])
            .MaximumLength(Domain.Stores.Constants.NameMaxLength)
                .WithMessage(localizer["Stores.v1.Update.Name.MaxLength {0}", Domain.Stores.Constants.NameMaxLength]);

        RuleFor(x => x.Description)
            .MaximumLength(Domain.Stores.Constants.DescriptionMaxLength)
                .WithMessage(localizer["Stores.v1.Update.Description.MaxLength {0}", Domain.Stores.Constants.DescriptionMaxLength]);
    }
}
