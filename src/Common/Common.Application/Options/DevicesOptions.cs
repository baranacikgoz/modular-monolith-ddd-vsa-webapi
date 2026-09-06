using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

/// <summary>Allow-list of client application ids accepted when a device registers a session (mobile-app-1, web-app-1, ...).</summary>
public class DevicesOptions
{
    public required IReadOnlyCollection<string> AllowedClientIds { get; init; }

    /// <summary>Cron for the recurring job that deactivates device registrations whose Keycloak session no
    /// longer exists (idle expiry, admin console, reuse detection all revoke a session without telling Notifications).</summary>
    public required string ReconcileCron { get; init; }

    /// <summary>Distinct users reconciled against Keycloak per recurring job run.</summary>
    public required int ReconcileBatchSize { get; init; }
}

public class DevicesOptionsValidator : CustomValidator<DevicesOptions>
{
    public DevicesOptionsValidator()
    {
        RuleFor(o => o.AllowedClientIds)
            .NotEmpty()
            .WithMessage("AllowedClientIds must not be empty.");

        RuleForEach(o => o.AllowedClientIds)
            .NotEmpty()
            .WithMessage("AllowedClientIds entries must not be empty.");

        RuleFor(o => o.ReconcileCron)
            .NotEmpty()
            .WithMessage("ReconcileCron must not be empty.");

        RuleFor(o => o.ReconcileBatchSize)
            .GreaterThan(0)
            .WithMessage("ReconcileBatchSize must be greater than 0.");
    }
}
