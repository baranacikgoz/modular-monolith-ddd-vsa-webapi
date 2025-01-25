using Common.Application.CQS;
using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using IAM.Application.Common.Validations;
using Microsoft.Extensions.Localization;

namespace IAM.Application.OTP.Features.VerifyThenRemove;

public sealed record VerifyThenRemoveOtpCommand(string PhoneNumber, string Otp) : ICommand;

public sealed class VerifyThenRemoveOtpCommandValidator : CustomValidator<VerifyThenRemoveOtpCommand>
{
    public VerifyThenRemoveOtpCommandValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
               .WithMessage(localizer["Users.OTP.VerifyThenRemove.PhoneNumber.NotEmpty"]);
        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer)
        .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
        RuleFor(x => x.Otp)
            .NotEmpty()
               .WithMessage(localizer["Users.OTP.VerifyThenRemove.Otp.NotEmpty"]);
    }
}
