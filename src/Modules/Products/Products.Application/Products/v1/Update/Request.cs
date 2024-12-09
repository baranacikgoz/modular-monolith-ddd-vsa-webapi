using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Products.Application.Products.v1.Update;

public sealed record Request(string? Name, string? Description);

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage(localizer["Products.v1.Update.Name.NotEmpty"])
            .MaximumLength(Domain.Products.Constants.NameMaxLength)
                .WithMessage(localizer["Products.v1.Update.Name.MaxLength {0}", Domain.Products.Constants.NameMaxLength])
            .When(x => x.Name is not null);

        RuleFor(x => x.Description)
            .NotEmpty()
                .WithMessage(localizer["Products.v1.Update.Description.NotEmpty"])
            .MaximumLength(Domain.Products.Constants.DescriptionMaxLength)
                .WithMessage(localizer["Products.v1.Update.Description.MaxLength {0}", Domain.Products.Constants.DescriptionMaxLength])
            .When(x => x.Description is not null);
    }
}
