using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public sealed class ReverseProxyOptions
{
    public bool IsEnabled { get; init; } = true;

    /// <summary>
    /// CIDR ranges of trusted upstream proxy networks (e.g., "10.0.0.0/8" for a K8s pod network).
    /// When empty, only loopback is trusted (ASP.NET Core default).
    /// Override per environment: ReverseProxyOptions__TrustedNetworks__0=10.0.0.0/8
    /// </summary>
    public IReadOnlyList<string> TrustedNetworks { get; init; } = [];

    public int ForwardLimit { get; init; } = 1;
}

public sealed class ReverseProxyOptionsValidator : CustomValidator<ReverseProxyOptions>
{
    public ReverseProxyOptionsValidator()
    {
        RuleFor(o => o.ForwardLimit)
            .GreaterThan(0)
            .WithMessage("ForwardLimit must be greater than 0.");

        // GetIpAddress() trusts only Connection.RemoteIpAddress, which UseForwardedHeaders only rewrites
        // for peers inside TrustedNetworks. An empty list here means every client — including behind a
        // real reverse proxy — collapses into one partition (the proxy's own IP), so rate limiting can
        // lock out all traffic behind a single bucket.
        RuleFor(o => o.TrustedNetworks)
            .NotEmpty()
            .When(o => o.IsEnabled)
            .WithMessage("TrustedNetworks must not be empty when IsEnabled is true.");

        RuleForEach(o => o.TrustedNetworks)
            .Must(BeValidCidr)
            .WithMessage("Each TrustedNetwork entry must be valid CIDR notation (e.g., '10.0.0.0/8').");
    }

    private static bool BeValidCidr(string cidr)
    {
        var parts = cidr.Split('/');
        return parts.Length == 2
               && System.Net.IPAddress.TryParse(parts[0], out _)
               && int.TryParse(parts[1], out var prefix)
               && prefix is >= 0 and <= 128;
    }
}
