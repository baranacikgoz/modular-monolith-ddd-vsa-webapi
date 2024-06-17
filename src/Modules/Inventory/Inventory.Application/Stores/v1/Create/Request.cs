using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Common.Application.Localization;
using Common.Application.Validation;
using Common.Domain.StronglyTypedIds;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Inventory.Application.Stores.v1.Create;

public sealed record Request(string Name, string Description)
{
    [JsonConverter(typeof(StronglyTypedIdReadOnlyJsonConverter<ApplicationUserId>))]
    public ApplicationUserId OwnerId { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.OwnerId)
            .NotEmpty()
                .WithMessage(localizer["v1.Create.OwnerId.NotEmpty"]);

        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage(localizer["v1.Create.Name.NotEmpty"])
            .MaximumLength(Domain.Stores.Constants.NameMaxLength)
                .WithMessage(localizer["v1.Create.Name.MaxLength", Domain.Stores.Constants.NameMaxLength]);

        RuleFor(x => x.Description)
            .MaximumLength(Domain.Stores.Constants.DescriptionMaxLength)
                .WithMessage(localizer["v1.Create.Description.MaxLength", Domain.Stores.Constants.DescriptionMaxLength]);
    }
}
