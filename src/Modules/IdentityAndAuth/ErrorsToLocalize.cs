using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using IdentityAndAuth.Features.Captcha;
using IdentityAndAuth.Features.Captcha.Errors;
using IdentityAndAuth.Features.Tokens.Errors;
using IdentityAndAuth.Features.Users.Domain.Errors;
using IdentityAndAuth.Features.Users.Services.Otp;
using IdentityAndAuth.Features.Users.Services.Otp.Errors;
using IdentityAndAuth.Features.Users.Services.PhoneVerificationToken;
using IdentityAndAuth.Features.Users.Services.PhoneVerificationToken.Errors;
using IdentityAndAuth.Identity;
using IdentityAndAuth.Identity.Errors;
using Microsoft.Extensions.Localization;

namespace IdentityAndAuth;

public static class ErrorsToLocalize
{
    public static Dictionary<string, Func<IStringLocalizer<IErrorTranslator>, string>> GetErrorsAndMessages()
    {
        return new Dictionary<string, Func<IStringLocalizer<IErrorTranslator>, string>>
        {
            { nameof(UserErrors.NotFound), (localizer) => localizer["Kullanıcı bulunamadı."] },
            { nameof(IdentityErrors.Some), (localizer) => localizer["Bir veya daha fazla hata oluştu."] },
            { nameof(OtpErrors.InvalidOtp), (localizer) => localizer["Kod hatalı veya süresi dolmuş."] },
            { nameof(TokenErrors.InvalidToken), (localizer) => localizer["Geçersiz token."] },
            { nameof(TokenErrors.InvalidRefreshToken), (localizer) => localizer["Geçersiz yenileme tokeni."] },
            { nameof(CaptchaErrors.NotHuman), (localizer) => localizer["Robot olmadığınız doğrulanamadı."] },
            { nameof(CaptchaErrors.ServiceUnavailable), (localizer) => localizer["Captcha servisi kaynaklı hata. Lütfen tekrar deneyiniz."] },
            { nameof(PhoneVerificationTokenErrors.VerificationFailed), (localizer) => localizer["Telefon doğrulama tokeni doğrulaması başarısız."] }
        };
    }
}
