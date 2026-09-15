using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

/// <summary>
/// Which identifier Keycloak usernames are provisioned from, and therefore which self-service identity
/// endpoints exist at all. A deployment-time decision, not a runtime toggle: IAM's module registration
/// reads it once at startup to conditionally map routes, so changing it requires a restart, the same as
/// the module-enablement list it mirrors.
/// </summary>
public enum UsernameSource
{
    PhoneNumber,
    Email
}

public class UserIdentityOptions
{
    public required UsernameSource UsernameSource { get; set; }
}

public class UserIdentityOptionsValidator : CustomValidator<UserIdentityOptions>
{
    public UserIdentityOptionsValidator()
    {
        RuleFor(o => o.UsernameSource)
            .IsInEnum();
    }
}
