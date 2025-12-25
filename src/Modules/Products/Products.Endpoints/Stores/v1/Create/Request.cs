using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Common.Application.Localization;
using Common.Application.Validation;
using Common.Domain.StronglyTypedIds;
using FluentValidation;
using Microsoft.Extensions.Localization;
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
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.OwnerId)
            .NotEmpty()
            .WithMessage(localizer["Stores.v1.Create.OwnerId.NotEmpty"]);

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(localizer["Stores.v1.Create.Name.NotEmpty"])
            .MaximumLength(Constants.NameMaxLength)
            .WithMessage(localizer["Stores.v1.Create.Name.MaxLength {0}", Constants.NameMaxLength]);

        RuleFor(x => x.Description)
            .MaximumLength(Constants.DescriptionMaxLength)
            .WithMessage(localizer["Stores.v1.Create.Description.MaxLength {0}", Constants.DescriptionMaxLength]);

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage(localizer["Stores.v1.Create.Address.NotEmpty"])
            .MaximumLength(Constants.AddressMaxLength)
            .WithMessage(localizer["Stores.v1.Create.Address.MaxLength {0}", Constants.AddressMaxLength]);
    }
}
