using Common.Application.Localization;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.Update;

public sealed record Request
{
    [FromRoute, ModelBinder<StronglyTypedIdBinder<StoreId>>]
    public required StoreId Id { get; init; }

    [FromBody]
    public required RequestBody Body { get; init; }

    public class RequestBody
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
    }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer["Stores.Update.Id.NotEmpty"]);

        RuleFor(x => x.Body)
            .NotEmpty()
            .WithMessage(localizer["Stores.Update.Body.NotEmpty"])
            .SetValidator(new RequestBodyValidator(localizer));
    }
}

public sealed class RequestBodyValidator : CustomValidator<Request.RequestBody>
{
    public RequestBodyValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.Name) || !string.IsNullOrWhiteSpace(x.Description) || !string.IsNullOrWhiteSpace(x.Address))
            .WithMessage(localizer["Stores.Update.AtLeastOnePropertyIsRequired"]);

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(localizer["Stores.Update.Name.NotEmpty"])
            .MaximumLength(Constants.NameMaxLength)
            .WithMessage(localizer["Stores.Update.Name.MaxLength {0}", Constants.NameMaxLength])
            .When(x => x.Name is not null);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(localizer["Stores.Update.Description.NotEmpty"])
            .MaximumLength(Constants.DescriptionMaxLength)
            .WithMessage(localizer["Stores.Update.Description.MaxLength {0}", Constants.DescriptionMaxLength])
            .When(x => x.Description is not null);

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage(localizer["Stores.Update.Address.NotEmpty"])
            .MaximumLength(Constants.AddressMaxLength)
            .WithMessage(localizer["Stores.Update.Address.MaxLength {0}", Constants.AddressMaxLength])
            .When(x => x.Address is not null);
    }
}
