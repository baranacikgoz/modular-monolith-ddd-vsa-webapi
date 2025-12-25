using Common.Application.Localization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace IAM.Infrastructure.Identity;

internal class LocalizedIdentityErrorDescriber(
    IStringLocalizer<ResxLocalizer> localizer
) : IdentityErrorDescriber
{
    public override IdentityError ConcurrencyFailure()
    {
        return Create(nameof(ConcurrencyFailure));
    }

    public override IdentityError DefaultError()
    {
        return Create(nameof(DefaultError));
    }

    public override IdentityError InvalidEmail(string? email)
    {
        return Create(nameof(InvalidEmail), email ?? string.Empty);
    }

    // We use PhoneNumber as UserName
    public override IdentityError InvalidUserName(string? userName)
    {
        return Create(nameof(InvalidUserName), userName ?? string.Empty);
    }

    public override IdentityError DuplicateEmail(string email)
    {
        return Create(nameof(DuplicateEmail), email);
    }

    // We use PhoneNumber as UserName
    public override IdentityError DuplicateUserName(string userName)
    {
        return Create(nameof(DuplicateUserName), userName);
    }

    public override IdentityError UserNotInRole(string role)
    {
        return Create(nameof(UserNotInRole), role);
    }

    public override IdentityError DuplicateRoleName(string role)
    {
        return Create(nameof(DuplicateRoleName), role);
    }

    public override IdentityError InvalidRoleName(string? role)
    {
        return Create(nameof(InvalidRoleName), role ?? string.Empty);
    }

    public override IdentityError InvalidToken()
    {
        return Create(nameof(InvalidToken));
    }

    public override IdentityError LoginAlreadyAssociated()
    {
        return Create(nameof(LoginAlreadyAssociated));
    }

    public override IdentityError PasswordMismatch()
    {
        return Create(nameof(PasswordMismatch));
    }

    public override IdentityError PasswordRequiresDigit()
    {
        return Create(nameof(PasswordRequiresDigit));
    }

    public override IdentityError PasswordRequiresLower()
    {
        return Create(nameof(PasswordRequiresLower));
    }

    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        return Create(nameof(PasswordRequiresNonAlphanumeric));
    }

    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
    {
        return Create(nameof(PasswordRequiresUniqueChars), uniqueChars);
    }

    public override IdentityError PasswordRequiresUpper()
    {
        return Create(nameof(PasswordRequiresUpper));
    }

    public override IdentityError PasswordTooShort(int length)
    {
        return Create(nameof(PasswordTooShort), length);
    }

    public override IdentityError UserAlreadyHasPassword()
    {
        return Create(nameof(UserAlreadyHasPassword));
    }

    public override IdentityError UserAlreadyInRole(string role)
    {
        return Create(nameof(UserAlreadyInRole), role);
    }

    public override IdentityError RecoveryCodeRedemptionFailed()
    {
        return Create(nameof(RecoveryCodeRedemptionFailed));
    }

    public override IdentityError UserLockoutNotEnabled()
    {
        return Create(nameof(UserLockoutNotEnabled));
    }

    private IdentityError Create(string key, object? parameter = null)
    {
        if (parameter is null)
        {
            return new IdentityError { Code = key, Description = localizer[key] };
        }

        return new IdentityError { Code = key, Description = localizer[key, parameter] };
    }
}
