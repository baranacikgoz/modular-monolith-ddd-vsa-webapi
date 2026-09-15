using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

/// <summary>
/// Which identity scheme this deployment runs on: chosen once at project setup, not a runtime toggle.
/// It decides which self-service registration/login endpoints exist at all (IAM's module registration
/// reads it once at startup to conditionally map routes, so changing it requires a restart, the same as
/// the module-enablement list it mirrors) and, as a consequence, which identifier Keycloak usernames are
/// provisioned from.
/// </summary>
public enum IdentityScheme
{
    /// <summary>Passwordless: phone number + SMS OTP for registration and login.</summary>
    PhoneNumber,

    /// <summary>Email + password, with an email OTP verification step on every registration and login.</summary>
    Email
}

public class IdentitySchemeOptions
{
    public required IdentityScheme Scheme { get; set; }
}

public class IdentitySchemeOptionsValidator : CustomValidator<IdentitySchemeOptions>
{
    public IdentitySchemeOptionsValidator()
    {
        RuleFor(o => o.Scheme)
            .IsInEnum();
    }
}
