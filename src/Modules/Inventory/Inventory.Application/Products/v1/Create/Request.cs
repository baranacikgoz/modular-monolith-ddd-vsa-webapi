using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Inventory.Application.Products.v1.Create;

public sealed record Request(string Name, string Description);

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage(localizer["Products.v1.Create.Name.NotEmpty"])
            .MaximumLength(Domain.Products.Constants.NameMaxLength)
                .WithMessage(localizer["Products.v1.Create.Name.MaxLength", Domain.Products.Constants.NameMaxLength]);

        RuleFor(x => x.Description)
            .NotEmpty()
                .WithMessage(localizer["Products.v1.Create.Description.NotEmpty"])
            .MaximumLength(Domain.Products.Constants.DescriptionMaxLength)
                .WithMessage(localizer["Products.v1.Create.Description.MaxLength", Domain.Products.Constants.DescriptionMaxLength]);
    }
}
