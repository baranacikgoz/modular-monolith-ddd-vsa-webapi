using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.My.Update;

public sealed record Request
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? Address { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.Name) || !string.IsNullOrWhiteSpace(x.Description) || !string.IsNullOrWhiteSpace(x.Address))
            .WithMessage(localizer["Stores.My.Update.AtLeastOnePropertyIsRequired"]);

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(localizer["Stores.My.Update.Name.NotEmpty"])
            .MaximumLength(Constants.NameMaxLength)
            .WithMessage(localizer["Stores.My.Update.Name.MaxLength {0}", Constants.NameMaxLength])
            .When(x => x.Name is not null);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(localizer["Stores.My.Update.Description.NotEmpty"])
            .MaximumLength(Constants.DescriptionMaxLength)
            .WithMessage(localizer["Stores.My.Update.Description.MaxLength {0}", Constants.DescriptionMaxLength])
            .When(x => x.Description is not null);

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage(localizer["Stores.My.Update.Address.NotEmpty"])
            .MaximumLength(Constants.AddressMaxLength)
            .WithMessage(localizer["Stores.My.Update.Address.MaxLength {0}", Constants.AddressMaxLength])
            .When(x => x.Address is not null);
    }
}
