using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.Update;

public sealed record Request
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<StoreId>>]
    public required StoreId Id { get; init; }

    [FromBody] public required RequestBody Body { get; init; }

    public class RequestBody
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
    }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer.Stores_Update_Id_NotEmpty);

        RuleFor(x => x.Body)
            .NotEmpty()
            .WithMessage(localizer.Stores_Update_Body_NotEmpty)
            .SetValidator(new RequestBodyValidator(localizer));
    }
}

public sealed class RequestBodyValidator : CustomValidator<Request.RequestBody>
{
    public RequestBodyValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.Name) || !string.IsNullOrWhiteSpace(x.Description) ||
                       !string.IsNullOrWhiteSpace(x.Address))
            .WithMessage(localizer.Stores_Update_AtLeastOnePropertyIsRequired);

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(localizer.Stores_Update_Name_NotEmpty)
            .MaximumLength(Constants.NameMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Stores_Update_Name_MaxLength,
                Constants.NameMaxLength))
            .When(x => x.Name is not null);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(localizer.Stores_Update_Description_NotEmpty)
            .MaximumLength(Constants.DescriptionMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Stores_Update_Description_MaxLength,
                Constants.DescriptionMaxLength))
            .When(x => x.Description is not null);

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage(localizer.Stores_Update_Address_NotEmpty)
            .MaximumLength(Constants.AddressMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Stores_Update_Address_MaxLength,
                Constants.AddressMaxLength))
            .When(x => x.Address is not null);
    }
}
