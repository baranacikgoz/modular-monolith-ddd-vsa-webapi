using Common.Application.Localization.Resources;
using Microsoft.AspNetCore.Identity;

namespace IAM.Infrastructure.Identity;

internal class LocalizedIdentityErrorDescriber(
    IResxLocalizer localizer
) : IdentityErrorDescriber
{
    public override IdentityError ConcurrencyFailure() => new()
    {
        Code = nameof(ConcurrencyFailure),
        Description = localizer.ConcurrencyFailure
    };

    public override IdentityError DefaultError() => new()
    {
        Code = nameof(DefaultError),
        Description = localizer.DefaultError
    };

    public override IdentityError InvalidEmail(string? email) => new()
    {
        Code = nameof(InvalidEmail),
        Description = string.Format(System.Globalization.CultureInfo.CurrentCulture, localizer.InvalidEmail,
            email ?? string.Empty)
    };

    public override IdentityError InvalidUserName(string? userName) => new()
    {
        Code = nameof(InvalidUserName),
        Description = string.Format(System.Globalization.CultureInfo.CurrentCulture, localizer.InvalidUserName,
            userName ?? string.Empty)
    };

    public override IdentityError DuplicateEmail(string email) => new()
    {
        Code = nameof(DuplicateEmail),
        Description = string.Format(System.Globalization.CultureInfo.CurrentCulture, localizer.DuplicateEmail, email)
    };

    public override IdentityError DuplicateUserName(string userName) => new()
    {
        Code = nameof(DuplicateUserName),
        Description = string.Format(System.Globalization.CultureInfo.CurrentCulture, localizer.DuplicateUserName,
            userName)
    };

    public override IdentityError UserNotInRole(string role) => new()
    {
        Code = nameof(UserNotInRole),
        Description = string.Format(System.Globalization.CultureInfo.CurrentCulture, localizer.UserNotInRole, role)
    };

    public override IdentityError DuplicateRoleName(string role) => new()
    {
        Code = nameof(DuplicateRoleName),
        Description = string.Format(System.Globalization.CultureInfo.CurrentCulture, localizer.DuplicateRoleName, role)
    };

    public override IdentityError InvalidRoleName(string? role) => new()
    {
        Code = nameof(InvalidRoleName),
        Description = string.Format(System.Globalization.CultureInfo.CurrentCulture, localizer.InvalidRoleName,
            role ?? string.Empty)
    };

    public override IdentityError InvalidToken() => new()
    {
        Code = nameof(InvalidToken),
        Description = localizer.InvalidToken
    };

    public override IdentityError LoginAlreadyAssociated() => new()
    {
        Code = nameof(LoginAlreadyAssociated),
        Description = localizer.LoginAlreadyAssociated
    };

    public override IdentityError PasswordMismatch() => new()
    {
        Code = nameof(PasswordMismatch),
        Description = localizer.PasswordMismatch
    };

    public override IdentityError PasswordRequiresDigit() => new()
    {
        Code = nameof(PasswordRequiresDigit),
        Description = localizer.PasswordRequiresDigit
    };

    public override IdentityError PasswordRequiresLower() => new()
    {
        Code = nameof(PasswordRequiresLower),
        Description = localizer.PasswordRequiresLower
    };

    public override IdentityError PasswordRequiresNonAlphanumeric() => new()
    {
        Code = nameof(PasswordRequiresNonAlphanumeric),
        Description = localizer.PasswordRequiresNonAlphanumeric
    };

    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars) => new()
    {
        Code = nameof(PasswordRequiresUniqueChars),
        Description = string.Format(System.Globalization.CultureInfo.CurrentCulture,
            localizer.PasswordRequiresUniqueChars, uniqueChars)
    };

    public override IdentityError PasswordRequiresUpper() => new()
    {
        Code = nameof(PasswordRequiresUpper),
        Description = localizer.PasswordRequiresUpper
    };

    public override IdentityError PasswordTooShort(int length) => new()
    {
        Code = nameof(PasswordTooShort),
        Description = string.Format(System.Globalization.CultureInfo.CurrentCulture, localizer.PasswordTooShort, length)
    };

    public override IdentityError UserAlreadyHasPassword() => new()
    {
        Code = nameof(UserAlreadyHasPassword),
        Description = localizer.UserAlreadyHasPassword
    };

    public override IdentityError UserAlreadyInRole(string role) => new()
    {
        Code = nameof(UserAlreadyInRole),
        Description = string.Format(System.Globalization.CultureInfo.CurrentCulture, localizer.UserAlreadyInRole, role)
    };

    public override IdentityError RecoveryCodeRedemptionFailed() => new()
    {
        Code = nameof(RecoveryCodeRedemptionFailed),
        Description = localizer.RecoveryCodeRedemptionFailed
    };

    public override IdentityError UserLockoutNotEnabled() => new()
    {
        Code = nameof(UserLockoutNotEnabled),
        Description = localizer.UserLockoutNotEnabled
    };
}
