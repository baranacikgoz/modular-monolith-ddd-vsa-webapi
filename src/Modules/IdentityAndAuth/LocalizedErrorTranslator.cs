using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using IdentityAndAuth.Features.Captcha;
using IdentityAndAuth.Features.Tokens.Errors;
using IdentityAndAuth.Features.Users.Domain.Errors;
using IdentityAndAuth.Features.Users.Services.Otp;
using IdentityAndAuth.Features.Users.Services.PhoneVerificationToken;
using IdentityAndAuth.Identity;
using Microsoft.Extensions.Localization;

namespace IdentityAndAuth;

public class LocalizedErrorTranslator(IStringLocalizer<LocalizedErrorTranslator> localizer) : IErrorTranslator
{
    public string Translate(Failure failure)
    {
        return failure switch
        {
            UserNotFoundError _ => localizer["Kullanıcı bulunamadı."],
            IdentityError _ => localizer["Bir veya daha fazla hata oluştu."],
            InvalidOtpError er => localizer["'{0}' numarası için geçersiz kod.", er.PhoneNumber],
            InvalidTokenError _ => localizer["Geçersiz token."],
            InvalidRefreshTokenError _ => localizer["Geçersiz yenileme tokeni."],
            CaptchaResultFailedError _ => localizer["Captcha doğrulaması başarısız."],
            PhoneVerificationTokenValidationFailedError _ => localizer["Telefon doğrulama tokeni doğrulaması başarısız."],
            _ => throw new InvalidOperationException($"Unknown error type: {failure.GetType().Name}")
        };
    }
}
