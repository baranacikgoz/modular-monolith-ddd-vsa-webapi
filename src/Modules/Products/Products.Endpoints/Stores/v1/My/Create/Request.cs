using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Products.Endpoints.Stores.v1.My.Create;

public sealed record Request(string Name, string Description, string Address);

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage(localizer["Stores.v1.My.Create.Name.NotEmpty"])
            .MaximumLength(Domain.Stores.Constants.NameMaxLength)
                .WithMessage(localizer["Stores.v1.My.Create.Name.MaxLength {0}", Domain.Stores.Constants.NameMaxLength]);

        RuleFor(x => x.Description)
            .NotEmpty()
                .WithMessage(localizer["Stores.v1.My.Create.Description.NotEmpty"])
            .MaximumLength(Domain.Stores.Constants.DescriptionMaxLength)
                .WithMessage(localizer["Stores.v1.My.Create.Description.MaxLength {0}", Domain.Stores.Constants.DescriptionMaxLength]);

        RuleFor(x => x.Address)
            .NotEmpty()
                .WithMessage(localizer["Stores.v1.My.Create.Address.NotEmpty"])
            .MaximumLength(Domain.Stores.Constants.AddressMaxLength)
                .WithMessage(localizer["Stores.v1.My.Create.Address.MaxLength {0}", Domain.Stores.Constants.AddressMaxLength]);
    }
}
