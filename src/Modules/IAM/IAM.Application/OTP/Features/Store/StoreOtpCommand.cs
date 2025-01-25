using Common.Application.CQS;
using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using IAM.Application.Common.Validations;
using Microsoft.Extensions.Localization;

namespace IAM.Application.OTP.Features.Store;

public sealed record StoreOtpCommand(string PhoneNumber, string Otp) : ICommand;

public sealed class StoreOtpCommandValidator : CustomValidator<StoreOtpCommand>
{
    public StoreOtpCommandValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
               .WithMessage(localizer["Users.OTP.Store.PhoneNumber.NotEmpty"]);
        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer)

        .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
        RuleFor(x => x.Otp)
            .NotEmpty()
               .WithMessage(localizer["Users.OTP.Store.Otp.NotEmpty"]);
    }
}
