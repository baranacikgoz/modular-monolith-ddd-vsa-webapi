using Common.Application.CQS;
using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using IAM.Application.Common.Validations;
using Microsoft.Extensions.Localization;

namespace IAM.Application.OTP.Features.Send;

public sealed record SendOtpCommand(string PhoneNumber, string Otp) : ICommand;

public class SendOtpCommandValidator : CustomValidator<SendOtpCommand>
{
    public SendOtpCommandValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
               .WithMessage(localizer["Users.OTP.Send.PhoneNumber.NotEmpty"]);

        RuleFor(x => x.PhoneNumber)
            .PhoneNumberValidation(localizer)
        .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

        RuleFor(x => x.Otp)
            .NotEmpty()
               .WithMessage(localizer["Users.OTP.Send.Otp.NotEmpty"]);
    }
}
