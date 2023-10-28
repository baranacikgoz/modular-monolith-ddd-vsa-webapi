using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using IdentityAndAuth.Features.Captcha;
using IdentityAndAuth.Features.Tokens.Errors;
using IdentityAndAuth.Features.Users.Domain.Errors;
using IdentityAndAuth.Features.Users.Services.Otp;
using IdentityAndAuth.Features.Users.Services.PhoneVerificationToken;
using IdentityAndAuth.Features.Users.Services.PhoneVerificationToken.Errors;
using IdentityAndAuth.Identity;
using Microsoft.Extensions.Localization;

namespace IdentityAndAuth;

public class LocalizedErrorTranslator(IStringLocalizer<LocalizedErrorTranslator> localizer) : IErrorTranslator
{
    private readonly Dictionary<string, Func<string>> _errorKeyToMessage = new()
    {
        { nameof(UserErrors.NotFound), () => localizer["Kullanıcı bulunamadı."] },
        { nameof(IdentityErrors.Some), () => localizer["Bir veya daha fazla hata oluştu."] },
        { nameof(OtpErrors.InvalidOtp), () => localizer["Kod hatalı veya süresi dolmuş."] },
        { nameof(TokenErrors.InvalidToken), () => localizer["Geçersiz token."] },
        { nameof(TokenErrors.InvalidRefreshToken), () => localizer["Geçersiz yenileme tokeni."] },
        { nameof(CaptchaErrors.NotHuman), () => localizer["Robot olmadığınız doğrulanamadı."] },
        { nameof(CaptchaErrors.ServiceUnavailable), () => localizer["Captcha servisi kaynaklı hata. Lütfen tekrar deneyiniz."] },
        { nameof(PhoneVerificationTokenErrors.VerificationFailed), () => localizer["Telefon doğrulama tokeni doğrulaması başarısız."] }
    };
    public string Translate(Error error)
    {
        if (_errorKeyToMessage.TryGetValue(error.Key, out var message))
        {
            return message();
        }

        throw new NotImplementedException($"Error key {error.Key} is not implemented.");
    }
}
