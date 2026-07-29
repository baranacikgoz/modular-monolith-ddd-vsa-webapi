using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public enum PushProvider
{
    /// <summary>No-op, logs instead of sending. Non-production only.</summary>
    Dummy,
    Firebase
}

public class PushOptions
{
    /// <summary>"Dummy" (non-production only, no-op) or "Firebase".</summary>
    public required PushProvider Provider { get; set; }

    /// <summary>Google service-account credentials for FCM. Required when Provider is Firebase.</summary>
    public FirebaseServiceAccountOptions? ServiceAccount { get; set; }

    /// <summary>Timeout for a single FCM multicast send call.</summary>
    public int SendTimeoutSeconds { get; set; }
}

/// <summary>
/// Typed mirror of a Google service-account JSON key. Kept as a nested object (not an escaped JSON
/// string) so it round-trips through Vault's native-JSON config delivery without needing the
/// PrivateKey's embedded newlines escaped.
/// </summary>
public class FirebaseServiceAccountOptions
{
    public string? Type { get; set; }
    public string? ProjectId { get; set; }
    public string? PrivateKeyId { get; set; }
    public string? PrivateKey { get; set; }
    public string? ClientEmail { get; set; }
    public string? ClientId { get; set; }
    public string? TokenUri { get; set; }
}

public class PushOptionsValidator : CustomValidator<PushOptions>
{
    public PushOptionsValidator()
    {
        // DummyPushGateway is a no-op: pushes are generated but never reach the device. In
        // Production that silently bricks every push flow, so fail fast until Provider is switched
        // to Firebase (with real credentials) below.
        RuleFor(o => o.Provider)
            .Must((_, provider, context) => !context.IsProduction() || provider != PushProvider.Dummy)
            .WithMessage(
                $"{nameof(PushOptions)}.{nameof(PushOptions.Provider)} is 'Dummy' in Production. Dummy push gateway is a " +
                "no-op — notifications would never reach devices. " +
                $"Set {nameof(PushOptions.Provider)} to 'Firebase' (with real credentials) before deploying.");

        When(o => o.Provider == PushProvider.Firebase, () =>
        {
            RuleFor(o => o.ServiceAccount)
                .NotNull()
                .WithMessage("ServiceAccount must not be null when Provider is Firebase.");

            When(o => o.ServiceAccount is not null, () =>
            {
                RuleFor(o => o.ServiceAccount!.ProjectId)
                    .NotEmpty()
                    .WithMessage("ServiceAccount.ProjectId must not be empty when Provider is Firebase.");

                RuleFor(o => o.ServiceAccount!.PrivateKey)
                    .NotEmpty()
                    .WithMessage("ServiceAccount.PrivateKey must not be empty when Provider is Firebase.");

                RuleFor(o => o.ServiceAccount!.ClientEmail)
                    .NotEmpty()
                    .WithMessage("ServiceAccount.ClientEmail must not be empty when Provider is Firebase.");
            });

            RuleFor(o => o.SendTimeoutSeconds)
                .GreaterThan(0)
                .WithMessage("SendTimeoutSeconds must be greater than 0.");
        });
    }
}
