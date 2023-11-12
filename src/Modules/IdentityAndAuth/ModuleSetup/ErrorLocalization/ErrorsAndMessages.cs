using Common.Core.Contracts;
using IdentityAndAuth.Features.Captcha.Domain.Errors;
using IdentityAndAuth.Features.Identity.Domain.Errors;
using IdentityAndAuth.Features.Tokens.Domain.Errors;
using Microsoft.Extensions.Localization;

namespace IdentityAndAuth.ModuleSetup.ErrorLocalization;

public static class ErrorsAndMessages
{
    public static Dictionary<string, Func<IStringLocalizer<IErrorTranslator>, string>> Get()
        => new()
        {
            { nameof(UserErrors.NotFound), localizer => localizer["Kullanıcı bulunamadı."] },
            { nameof(IdentityErrors.Some), localizer => localizer["Bir veya daha fazla hata oluştu."] },
            { nameof(OtpErrors.InvalidOtp), localizer => localizer["Kod hatalı veya süresi dolmuş."] },
            { nameof(TokenErrors.InvalidToken), localizer => localizer["Geçersiz token."] },
            { nameof(TokenErrors.InvalidRefreshToken), localizer => localizer["Geçersiz yenileme tokeni."] },
            { nameof(CaptchaErrors.NotHuman), localizer => localizer["Robot olmadığınız doğrulanamadı."] },
            { nameof(CaptchaErrors.ServiceUnavailable), localizer => localizer["Captcha servisi kaynaklı hata. Lütfen tekrar deneyiniz."] },
            { nameof(PhoneVerificationTokenErrors.VerificationFailed), localizer => localizer["Telefon doğrulama tokeni doğrulaması başarısız."] }
        };
}
