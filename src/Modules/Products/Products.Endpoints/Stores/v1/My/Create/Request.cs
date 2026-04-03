using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.Validation;
using FluentValidation;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.My.Create;

public sealed record Request(string Name, string Description, string Address);

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(localizer.Stores_v1_My_Create_Name_NotEmpty)
            .MaximumLength(Constants.NameMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Stores_v1_My_Create_Name_MaxLength,
                Constants.NameMaxLength));

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(localizer.Stores_v1_My_Create_Description_NotEmpty)
            .MaximumLength(Constants.DescriptionMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Stores_v1_My_Create_Description_MaxLength,
                Constants.DescriptionMaxLength));

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage(localizer.Stores_v1_My_Create_Address_NotEmpty)
            .MaximumLength(Constants.AddressMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Stores_v1_My_Create_Address_MaxLength,
                Constants.AddressMaxLength));
    }
}
