using System.Globalization;
using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Common.Application.Localization.Resources;
using Common.Application.Validation;
using Common.Domain.StronglyTypedIds;
using FluentValidation;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.Create;

public sealed record Request
{
    [JsonConverter(typeof(StronglyTypedIdReadOnlyJsonConverter<ApplicationUserId>))]
    public ApplicationUserId OwnerId { get; init; }

    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Address { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.OwnerId)
            .NotEmpty()
            .WithMessage(localizer.Stores_v1_Create_OwnerId_NotEmpty);

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(localizer.Stores_v1_Create_Name_NotEmpty)
            .MaximumLength(Constants.NameMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Stores_v1_Create_Name_MaxLength,
                Constants.NameMaxLength));

        RuleFor(x => x.Description)
            .MaximumLength(Constants.DescriptionMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Stores_v1_Create_Description_MaxLength,
                Constants.DescriptionMaxLength));

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage(localizer.Stores_v1_Create_Address_NotEmpty)
            .MaximumLength(Constants.AddressMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Stores_v1_Create_Address_MaxLength,
                Constants.AddressMaxLength));
    }
}
