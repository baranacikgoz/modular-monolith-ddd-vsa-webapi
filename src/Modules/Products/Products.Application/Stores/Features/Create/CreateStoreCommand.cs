using Common.Application.CQS;
using Common.Application.Localization;
using Common.Application.Validation;
using Common.Domain.StronglyTypedIds;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Products.Domain.Stores;

namespace Products.Application.Stores.Features.Create;

public sealed record CreateStoreCommand(ApplicationUserId OwnerId, string Name, string Description, string Address) : ICommand<StoreId>;

public sealed class CreateStoreCommandValidator : CustomValidator<CreateStoreCommand>
{
    public CreateStoreCommandValidator(IStringLocalizer<ResxLocalizer> localizer)
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
