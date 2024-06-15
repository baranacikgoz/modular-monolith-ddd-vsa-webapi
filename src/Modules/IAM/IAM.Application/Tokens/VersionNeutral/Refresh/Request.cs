using Common.Application.Localization;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace IAM.Application.Tokens.VersionNeutral.Refresh;

public sealed record Request(string ExpiredAccessToken, string RefreshToken);

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage(localizer["Yenileme tokeni boş olamaz."]);

        RuleFor(x => x.ExpiredAccessToken)
            .NotEmpty()
            .WithMessage(localizer["Eski erişim tokeni boş olamaz."]);
    }
}
