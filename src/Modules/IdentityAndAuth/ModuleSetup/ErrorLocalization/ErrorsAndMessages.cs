using Common.Core.Contracts;
using IdentityAndAuth.Features.Captcha.Domain.Errors;
using IdentityAndAuth.Features.Identity.Domain.Errors;
using IdentityAndAuth.Features.Tokens.Domain.Errors;
using Microsoft.Extensions.Localization;

namespace IdentityAndAuth.ModuleSetup.ErrorLocalization;

public static class ErrorsAndMessages
{
    public static IEnumerable<KeyValuePair<string, Func<IStringLocalizer<IErrorTranslator>, string>>> Get()
    {
        yield return new(nameof(UserErrors.NotFound), localizer => localizer["Kullanıcı bulunamadı."]);
        yield return new(nameof(IdentityErrors.Some), localizer => localizer["Bir veya daha fazla hata oluştu."]);
        yield return new(nameof(OtpErrors.InvalidOtp), localizer => localizer["Kod hatalı veya süresi dolmuş."]);
        yield return new(nameof(TokenErrors.InvalidToken), localizer => localizer["Geçersiz token."]);
        yield return new(nameof(TokenErrors.InvalidRefreshToken), localizer => localizer["Geçersiz yenileme tokeni."]);
        yield return new(nameof(CaptchaErrors.NotHuman), localizer => localizer["Robot olmadığınız doğrulanamadı."]);
        yield return new(nameof(CaptchaErrors.ServiceUnavailable), localizer => localizer["Captcha servisi kaynaklı hata. Lütfen tekrar deneyiniz."]);
        yield return new(nameof(PhoneVerificationTokenErrors.VerificationFailed), localizer => localizer["Telefon doğrulama tokeni doğrulaması başarısız."]);
    }
}
