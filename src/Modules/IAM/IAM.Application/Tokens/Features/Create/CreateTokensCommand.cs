using Common.Application.CQS;
using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using IAM.Application.Common.Validations;
using IAM.Application.Tokens.DTOs;
using Microsoft.Extensions.Localization;

namespace IAM.Application.Tokens.Features.Create;

public sealed record CreateTokensCommand(string PhoneNumber) : ICommand<TokensDto>;

public sealed class CreateTokensCommandValidator : CustomValidator<CreateTokensCommand>
{
    public CreateTokensCommandValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
                .WithMessage(localizer["Tokens.Create.PhoneNumber.NotEmpty"]);

        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer)
        .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

    }
}
