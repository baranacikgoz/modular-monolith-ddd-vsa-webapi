using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

/// <summary>Master key of <c>SecretProtector</c>, the AES-GCM encryption of secrets a module stores (third-party credentials, generated webhook secrets).</summary>
public class SecretProtectionOptions
{
    /// <summary>
    ///     Base64 of exactly 32 random bytes (AES-256). Delivered by the deployment's secret store, never stored next
    ///     to the ciphertext. Changing it makes every value encrypted under the previous key undecryptable.
    /// </summary>
    public required string EncryptionKey { get; set; }
}

public class SecretProtectionOptionsValidator : CustomValidator<SecretProtectionOptions>
{
    /// <summary>The checked-in development key (base64 of an ASCII sentence). Valid only in the Development environment.</summary>
    public const string DevelopmentPlaceholderKey = "bG9jYWwtZGV2LW9ubHktc2VjcmV0LWtleS0wMDAwISE=";

    public const int KeySizeBytes = 32;

    public SecretProtectionOptionsValidator()
    {
        RuleFor(x => x.EncryptionKey)
            .NotEmpty()
            .WithMessage("EncryptionKey must not be empty.")
            .Must(BeABase64KeyOfTheRequiredSize)
            .WithMessage($"EncryptionKey must be base64 that decodes to exactly {KeySizeBytes} bytes.");

        RuleFor(x => x.EncryptionKey)
            .Must((_, key, context) => context.IsDevelopment() || key != DevelopmentPlaceholderKey)
            .WithMessage(
                $"{nameof(SecretProtectionOptions)}.{nameof(SecretProtectionOptions.EncryptionKey)} still holds the checked-in " +
                $"development key outside Development. Generate a new key and set {nameof(SecretProtectionOptions)}__{nameof(SecretProtectionOptions.EncryptionKey)}.");
    }

    private static bool BeABase64KeyOfTheRequiredSize(string? key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return false;
        }

        var buffer = new byte[key.Length];
        return Convert.TryFromBase64String(key, buffer, out var written) && written == KeySizeBytes;
    }
}
