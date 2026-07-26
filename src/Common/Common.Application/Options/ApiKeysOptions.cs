using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

/// <summary>
/// Machine-caller credentials (CI pipelines, webhooks, ...). Each entry authenticates via the
/// <c>X-Api-Key</c> header and carries an explicit permission grant — no role, no owning user.
/// Rotation/revocation is a config edit (Vault secret update), not a deploy.
/// </summary>
public class ApiKeysOptions
{
    // Not `required`: the config binder resolves an empty JSON array ("Keys": []) to null rather
    // than an empty collection, so every consumer — including this class itself — must tolerate null.
    public IReadOnlyList<ApiKeyEntry>? Keys { get; init; }
}

public class ApiKeyEntry
{
    /// <summary>Human-readable caller identity, e.g. "github-actions-release". Used in logs/traces only.</summary>
    public required string Name { get; init; }

    /// <summary>Lowercase hex-encoded SHA-256 hash of the raw key. The raw key is never stored.</summary>
    public required string KeyHash { get; init; }

    /// <summary>Permission names in <c>CustomPermission.NameFor</c> format, e.g. "Permissions.AppReleaseGates.Manage".</summary>
    public required IReadOnlyList<string> Permissions { get; init; }
}

public class ApiKeysOptionsValidator : CustomValidator<ApiKeysOptions>
{
    public ApiKeysOptionsValidator()
    {
        RuleForEach(o => o.Keys).SetValidator(new ApiKeyEntryValidator());

        RuleFor(o => o.Keys)
            .Must(keys =>
            {
                var list = keys ?? [];
                return list.Select(k => k.Name).Distinct(StringComparer.Ordinal).Count() == list.Count;
            })
            .WithMessage("Key names must be unique.");
    }
}

public class ApiKeyEntryValidator : AbstractValidator<ApiKeyEntry>
{
    public ApiKeyEntryValidator()
    {
        RuleFor(k => k.Name)
            .NotEmpty()
            .WithMessage("Name must not be empty.");

        RuleFor(k => k.KeyHash)
            .Matches("^[0-9a-f]{64}$")
            .WithMessage("KeyHash must be a lowercase 64-character hex-encoded SHA-256 hash.");

        RuleFor(k => k.Permissions)
            .NotEmpty()
            .WithMessage("Permissions must not be empty.");

        // *My permissions (ReadMy, UpdateMy, ...) are scoped by ICurrentUser.Id, which is empty for
        // an API-key principal — granting one would silently no-op or leak across callers.
        RuleForEach(k => k.Permissions)
            .Must(p => !p.Contains("My", StringComparison.Ordinal))
            .WithMessage("API keys cannot be granted a *My permission — those are scoped to a human caller's own identity.");
    }
}
