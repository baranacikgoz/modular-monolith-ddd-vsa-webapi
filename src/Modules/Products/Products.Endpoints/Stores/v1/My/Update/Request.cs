using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.Validation;
using FluentValidation;
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
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.Name) || !string.IsNullOrWhiteSpace(x.Description) ||
                       !string.IsNullOrWhiteSpace(x.Address))
            .WithMessage(localizer.Stores_My_Update_AtLeastOnePropertyIsRequired);

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(localizer.Stores_My_Update_Name_NotEmpty)
            .MaximumLength(Constants.NameMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Stores_My_Update_Name_MaxLength,
                Constants.NameMaxLength))
            .When(x => x.Name is not null);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(localizer.Stores_My_Update_Description_NotEmpty)
            .MaximumLength(Constants.DescriptionMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Stores_My_Update_Description_MaxLength,
                Constants.DescriptionMaxLength))
            .When(x => x.Description is not null);

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage(localizer.Stores_My_Update_Address_NotEmpty)
            .MaximumLength(Constants.AddressMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Stores_My_Update_Address_MaxLength,
                Constants.AddressMaxLength))
            .When(x => x.Address is not null);
    }
}
