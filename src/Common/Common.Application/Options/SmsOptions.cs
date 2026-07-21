using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public enum SmsProvider
{
    Dummy,
    Real
}

public class SmsOptions
{
    /// <summary>"Dummy" (non-production only, no-op) or "Real".</summary>
    public required SmsProvider Provider { get; set; }
}

public class SmsOptionsValidator : CustomValidator<SmsOptions>
{
    public SmsOptionsValidator()
    {
        // DummySmsGateway is a no-op: OTPs and welcome SMS are generated but never reach the user.
        // In Production that silently bricks every SMS flow, so fail fast until Provider is switched
        // to Real and a real ISmsGateway implementation is registered.
        RuleFor(o => o.Provider)
            .Must((_, provider, context) => !context.IsProduction() || provider != SmsProvider.Dummy)
            .WithMessage(
                $"{nameof(SmsOptions)}.{nameof(SmsOptions.Provider)} is 'Dummy' in Production. Dummy SMS gateway is a no-op " +
                "— OTPs and notifications would never reach users. " +
                $"Set {nameof(SmsOptions.Provider)} to 'Real' (with a real ISmsGateway implementation) before deploying.");
    }
}
