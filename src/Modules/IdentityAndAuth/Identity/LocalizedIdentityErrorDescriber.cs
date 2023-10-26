using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace IdentityAndAuth.Identity;

public class LocalizedIdentityErrorDescriber(
    IStringLocalizer<LocalizedIdentityErrorDescriber> localizer
    ) : IdentityErrorDescriber
{
    public override Microsoft.AspNetCore.Identity.IdentityError ConcurrencyFailure()
    {
        return new Microsoft.AspNetCore.Identity.IdentityError
        {
            Code = nameof(ConcurrencyFailure),
            Description = localizer["Aynı anda birden fazla istek bu kullanıcıyı güncellemeye çalıştı. Tekrar deneyiniz."]
        };
    }

    public override Microsoft.AspNetCore.Identity.IdentityError DefaultError()
    {
        return new Microsoft.AspNetCore.Identity.IdentityError
        {
            Code = nameof(DefaultError),
            Description = localizer["Bilinmeyen bir hata oluştu."]
        };
    }

    public override Microsoft.AspNetCore.Identity.IdentityError InvalidEmail(string? email)
    {
        return new Microsoft.AspNetCore.Identity.IdentityError
        {
            Code = nameof(InvalidEmail),
            Description = localizer["'{0}' geçersiz bir e-posta adresi.", email ?? string.Empty]
        };
    }

    // We use PhoneNumber as UserName
    public override Microsoft.AspNetCore.Identity.IdentityError InvalidUserName(string? userName)
    {
        return new Microsoft.AspNetCore.Identity.IdentityError
        {
            Code = nameof(InvalidUserName),
            Description = localizer["'{0}' geçersiz bir telefon numarası.", userName ?? string.Empty]
        };
    }
    public override Microsoft.AspNetCore.Identity.IdentityError DuplicateEmail(string email)
    {
        return new Microsoft.AspNetCore.Identity.IdentityError
        {
            Code = nameof(DuplicateEmail),
            Description = localizer["'{0}' e-posta adresi zaten kullanımda.", email]
        };
    }

    // We use PhoneNumber as UserName
    public override Microsoft.AspNetCore.Identity.IdentityError DuplicateUserName(string userName)
    {
        return new Microsoft.AspNetCore.Identity.IdentityError
        {
            Code = nameof(DuplicateUserName),
            Description = localizer["'{0}' telefon numarası zaten kullanımda.", userName]
        };
    }

    public override Microsoft.AspNetCore.Identity.IdentityError UserNotInRole(string role)
    {
        return new Microsoft.AspNetCore.Identity.IdentityError
        {
            Code = nameof(UserNotInRole),
            Description = localizer["Kullanıcı '{0}' rolüne sahip değil.", role]
        };
    }

    public override Microsoft.AspNetCore.Identity.IdentityError DuplicateRoleName(string role)
    {
        return new Microsoft.AspNetCore.Identity.IdentityError
        {
            Code = nameof(DuplicateRoleName),
            Description = localizer["'{0}' rolü zaten var.", role]
        };
    }

    public override Microsoft.AspNetCore.Identity.IdentityError InvalidRoleName(string? role)
    {
        return new Microsoft.AspNetCore.Identity.IdentityError
        {
            Code = nameof(InvalidRoleName),
            Description = localizer["'{0}' geçersiz bir rol adı.", role ?? string.Empty]
        };
    }

    public override Microsoft.AspNetCore.Identity.IdentityError InvalidToken()
    {
        return new Microsoft.AspNetCore.Identity.IdentityError
        {
            Code = nameof(InvalidToken),
            Description = localizer["Geçersiz token."]
        };
    }

    public override Microsoft.AspNetCore.Identity.IdentityError LoginAlreadyAssociated()
    {
        return new Microsoft.AspNetCore.Identity.IdentityError
        {
            Code = nameof(LoginAlreadyAssociated),
            Description = localizer["Bu kullanıcı zaten bir hesaba sahip."]
        };
    }

    public override Microsoft.AspNetCore.Identity.IdentityError PasswordMismatch()
    {
        return new Microsoft.AspNetCore.Identity.IdentityError
        {
            Code = nameof(PasswordMismatch),
            Description = localizer["Şifre yanlış."]
        };
    }

    public override Microsoft.AspNetCore.Identity.IdentityError PasswordRequiresDigit()
    {
        return new Microsoft.AspNetCore.Identity.IdentityError
        {
            Code = nameof(PasswordRequiresDigit),
            Description = localizer["Şifre en az bir rakam içermelidir ('0'-'9')."]
        };
    }

    public override Microsoft.AspNetCore.Identity.IdentityError PasswordRequiresLower()
    {
        return new Microsoft.AspNetCore.Identity.IdentityError
        {
            Code = nameof(PasswordRequiresLower),
            Description = localizer["Şifre en az bir küçük harf içermelidir ('a'-'z')."]
        };
    }

    public override Microsoft.AspNetCore.Identity.IdentityError PasswordRequiresNonAlphanumeric()
    {
        return new Microsoft.AspNetCore.Identity.IdentityError
        {
            Code = nameof(PasswordRequiresNonAlphanumeric),
            Description = localizer["Şifre en az bir alfanümerik olmayan karakter içermelidir."]
        };
    }

    public override Microsoft.AspNetCore.Identity.IdentityError PasswordRequiresUniqueChars(int uniqueChars)
    {
        return new Microsoft.AspNetCore.Identity.IdentityError
        {
            Code = nameof(PasswordRequiresUniqueChars),
            Description = localizer["Şifre en az {0} farklı karakter içermelidir.", uniqueChars]
        };
    }

    public override Microsoft.AspNetCore.Identity.IdentityError PasswordRequiresUpper()
    {
        return new Microsoft.AspNetCore.Identity.IdentityError
        {
            Code = nameof(PasswordRequiresUpper),
            Description = localizer["Şifre en az bir büyük harf içermelidir ('A'-'Z')."]
        };
    }

    public override Microsoft.AspNetCore.Identity.IdentityError PasswordTooShort(int length)
    {
        return new Microsoft.AspNetCore.Identity.IdentityError
        {
            Code = nameof(PasswordTooShort),
            Description = localizer["Şifre en az {0} karakter uzunluğunda olmalıdır.", length]
        };
    }

    public override Microsoft.AspNetCore.Identity.IdentityError UserAlreadyHasPassword()
    {
        return new Microsoft.AspNetCore.Identity.IdentityError
        {
            Code = nameof(UserAlreadyHasPassword),
            Description = localizer["Kullanıcı zaten bir şifreye sahip."]
        };
    }

    public override Microsoft.AspNetCore.Identity.IdentityError UserAlreadyInRole(string role)
    {
        return new Microsoft.AspNetCore.Identity.IdentityError
        {
            Code = nameof(UserAlreadyInRole),
            Description = localizer["Kullanıcı zaten '{0}' rolüne sahip.", role]
        };
    }

    public override Microsoft.AspNetCore.Identity.IdentityError RecoveryCodeRedemptionFailed()
    {
        return new Microsoft.AspNetCore.Identity.IdentityError
        {
            Code = nameof(RecoveryCodeRedemptionFailed),
            Description = localizer["Kurtarma kodu kullanılamadı."]
        };
    }

    public override Microsoft.AspNetCore.Identity.IdentityError UserLockoutNotEnabled()
    {
        return new Microsoft.AspNetCore.Identity.IdentityError
        {
            Code = nameof(UserLockoutNotEnabled),
            Description = localizer["Kullanıcı kilitleme etkin değil."]
        };
    }
}
