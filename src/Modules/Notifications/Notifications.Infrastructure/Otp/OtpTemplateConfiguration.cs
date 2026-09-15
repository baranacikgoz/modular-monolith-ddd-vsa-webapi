using Common.Application.Options;
using Microsoft.Extensions.Configuration;

namespace Notifications.Infrastructure.Otp;

internal static class OtpTemplateConfiguration
{
    /// <summary>
    ///     The OTP send handlers fall back to the default request culture's template when the caller's
    ///     language has none. If even that is missing, every send would 500 at runtime; fail at boot instead.
    ///     Read from configuration (not <c>IOptions</c>) because this runs during DI registration.
    /// </summary>
    public static void RequireOtpTemplateForDefaultCulture(
        this IConfiguration configuration, Func<IEnumerable<string>> configuredLanguages, string optionsName)
    {
        var defaultCulture = configuration
            .GetSection(nameof(ResxLocalizationOptions))
            .GetValue<string>(nameof(ResxLocalizationOptions.DefaultCulture));

        // Languages are read lazily: with no default culture configured (partial test configs) there is
        // nothing to check, and the Templates section may not even be bound.
        if (defaultCulture is null || configuredLanguages().Contains(defaultCulture, StringComparer.OrdinalIgnoreCase))
        {
            return;
        }

        throw new InvalidOperationException(
            $"{optionsName}:Templates:Otp has no entry for the default culture '{defaultCulture}' " +
            $"({nameof(ResxLocalizationOptions)}:{nameof(ResxLocalizationOptions.DefaultCulture)}). " +
            "Every OTP send falls back to that template, so it must exist.");
    }
}
