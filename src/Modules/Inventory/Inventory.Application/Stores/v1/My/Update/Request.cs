using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Inventory.Application.Stores.v1.My.Update;
internal sealed record Request(string? Name, string? Description);

internal sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<RequestValidator> localizer)
    {
        RuleFor(r => r.Name)
            .MaximumLength(Domain.Stores.Constants.NameMaxLength)
                .WithMessage(localizer["Stores.My.Update.NameMaxLength"])
            .When(r => !string.IsNullOrEmpty(r.Name));

        RuleFor(r => r.Description)
            .MaximumLength(Domain.Stores.Constants.DescriptionMaxLength)
                .WithMessage(localizer["Stores.My.Update.DescriptionMaxLength"])
            .When(r => !string.IsNullOrEmpty(r.Description));
    }
}
