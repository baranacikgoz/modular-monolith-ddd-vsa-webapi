using Common.Application.CQS;
using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Products.Domain.Stores;

namespace Products.Application.Stores.Features.Update;

public sealed record UpdateStoreCommand(StoreId Id, string? Name, string? Description, string? Address) : ICommand;

public sealed class UpdateStoreCommandValidator : CustomValidator<UpdateStoreCommand>
{
    public UpdateStoreCommandValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithMessage(localizer["Stores.Update.Id.NotEmpty"]);

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
