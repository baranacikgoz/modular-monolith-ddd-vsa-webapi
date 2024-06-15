using Common.Application.Localization;
using Microsoft.Extensions.Localization;

namespace IAM.Infrastructure.Identity;

internal class LocalizedIdentityErrorDescriber(
    IStringLocalizer<ResxLocalizer> localizer
    ) : Microsoft.AspNetCore.Identity.IdentityErrorDescriber
{
    public override Microsoft.AspNetCore.Identity.IdentityError ConcurrencyFailure()
        => Create(nameof(ConcurrencyFailure));

    public override Microsoft.AspNetCore.Identity.IdentityError DefaultError()
        => Create(nameof(DefaultError));

    public override Microsoft.AspNetCore.Identity.IdentityError InvalidEmail(string? email)
        => Create(nameof(InvalidEmail), email ?? string.Empty);

    // We use PhoneNumber as UserName
    public override Microsoft.AspNetCore.Identity.IdentityError InvalidUserName(string? userName)
        => Create(nameof(InvalidUserName), userName ?? string.Empty);
    public override Microsoft.AspNetCore.Identity.IdentityError DuplicateEmail(string email)
        => Create(nameof(DuplicateEmail), email);

    // We use PhoneNumber as UserName
    public override Microsoft.AspNetCore.Identity.IdentityError DuplicateUserName(string userName)
        => Create(nameof(DuplicateUserName), userName);

    public override Microsoft.AspNetCore.Identity.IdentityError UserNotInRole(string role)
        => Create(nameof(UserNotInRole), role);

    public override Microsoft.AspNetCore.Identity.IdentityError DuplicateRoleName(string role)
        => Create(nameof(DuplicateRoleName), role);

    public override Microsoft.AspNetCore.Identity.IdentityError InvalidRoleName(string? role)
        => Create(nameof(InvalidRoleName), role ?? string.Empty);

    public override Microsoft.AspNetCore.Identity.IdentityError InvalidToken()
        => Create(nameof(InvalidToken));

    public override Microsoft.AspNetCore.Identity.IdentityError LoginAlreadyAssociated()
        => Create(nameof(LoginAlreadyAssociated));

    public override Microsoft.AspNetCore.Identity.IdentityError PasswordMismatch()
        => Create(nameof(PasswordMismatch));

    public override Microsoft.AspNetCore.Identity.IdentityError PasswordRequiresDigit()
        => Create(nameof(PasswordRequiresDigit));

    public override Microsoft.AspNetCore.Identity.IdentityError PasswordRequiresLower()
        => Create(nameof(PasswordRequiresLower));

    public override Microsoft.AspNetCore.Identity.IdentityError PasswordRequiresNonAlphanumeric()
        => Create(nameof(PasswordRequiresNonAlphanumeric));

    public override Microsoft.AspNetCore.Identity.IdentityError PasswordRequiresUniqueChars(int uniqueChars)
        => Create(nameof(PasswordRequiresUniqueChars), uniqueChars);

    public override Microsoft.AspNetCore.Identity.IdentityError PasswordRequiresUpper()
        => Create(nameof(PasswordRequiresUpper));

    public override Microsoft.AspNetCore.Identity.IdentityError PasswordTooShort(int length)
        => Create(nameof(PasswordTooShort), length);

    public override Microsoft.AspNetCore.Identity.IdentityError UserAlreadyHasPassword()
        => Create(nameof(UserAlreadyHasPassword));

    public override Microsoft.AspNetCore.Identity.IdentityError UserAlreadyInRole(string role)
        => Create(nameof(UserAlreadyInRole), role);

    public override Microsoft.AspNetCore.Identity.IdentityError RecoveryCodeRedemptionFailed()
        => Create(nameof(RecoveryCodeRedemptionFailed));

    public override Microsoft.AspNetCore.Identity.IdentityError UserLockoutNotEnabled()
        => Create(nameof(UserLockoutNotEnabled));

    private Microsoft.AspNetCore.Identity.IdentityError Create(string key, object? parameter = null)
    {
        if (parameter is null)
        {
            return new Microsoft.AspNetCore.Identity.IdentityError
            {
                Code = key,
                Description = localizer[key]
            };
        }

        return new Microsoft.AspNetCore.Identity.IdentityError
        {
            Code = key,
            Description = localizer[key, parameter]
        };
    }
}
