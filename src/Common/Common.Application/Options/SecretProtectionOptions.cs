using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

/// <summary>Keys of <c>SecretProtector</c>, the AES-GCM encryption of secrets a module stores (third-party credentials, generated webhook secrets).</summary>
public class SecretProtectionOptions
{
    /// <summary>
    ///     Base64 of exactly 32 random bytes (AES-256). Delivered by the deployment's secret store, never stored next
    ///     to the ciphertext. Every new value is encrypted under this key. To rotate: move the old value to
    ///     <see cref="PreviousEncryptionKeys" />, set a new key here, re-protect the stored values
    ///     (<c>SecretProtector.NeedsReprotect</c> / <c>Reprotect</c>), then drop the old key from the list. A value
    ///     whose key is in neither place can no longer be decrypted.
    /// </summary>
    public required string EncryptionKey { get; set; }

    /// <summary>
    ///     Keys that protected stored values before the last rotations, same format as <see cref="EncryptionKey" />.
    ///     They are used to decrypt and re-protect only, never to encrypt. Empty when no rotation is in progress. The
    ///     initializer stays although the property is required: an empty JSON array
    ///     (<c>"PreviousEncryptionKeys": []</c>) binds to nothing.
    /// </summary>
    public required IReadOnlyList<string> PreviousEncryptionKeys { get; set; } = [];
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

        RuleFor(x => x.PreviousEncryptionKeys)
            .NotNull()
            .WithMessage("PreviousEncryptionKeys must not be null. Use an empty list when no rotation is in progress.");

        RuleForEach(x => x.PreviousEncryptionKeys)
            .Must(BeABase64KeyOfTheRequiredSize)
            .WithMessage($"Every PreviousEncryptionKeys entry must be base64 that decodes to exactly {KeySizeBytes} bytes.");

        RuleForEach(x => x.PreviousEncryptionKeys)
            .Must((_, key, context) => context.IsDevelopment() || key != DevelopmentPlaceholderKey)
            .WithMessage("PreviousEncryptionKeys still holds the checked-in development key outside Development.");

        RuleFor(x => x.PreviousEncryptionKeys)
            .Must((options, previous) => HasNoRepeatedKey(options.EncryptionKey, previous))
            .WithMessage("PreviousEncryptionKeys must not repeat a key or contain the current EncryptionKey.");
    }

    private static bool BeABase64KeyOfTheRequiredSize(string? key)
    {
        return TryDecodeKey(key) is not null;
    }

    private static bool HasNoRepeatedKey(string? currentKey, IReadOnlyList<string>? previousKeys)
    {
        var seen = new HashSet<string>(StringComparer.Ordinal);
        var current = TryDecodeKey(currentKey);
        if (current is not null)
        {
            seen.Add(Convert.ToHexString(current));
        }

        foreach (var key in previousKeys ?? [])
        {
            var decoded = TryDecodeKey(key);
            if (decoded is not null && !seen.Add(Convert.ToHexString(decoded)))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>The key bytes, or null when the text is not base64 that decodes to exactly <see cref="KeySizeBytes" /> bytes.</summary>
    public static byte[]? TryDecodeKey(string? key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return null;
        }

        var buffer = new byte[key.Length];
        return Convert.TryFromBase64String(key, buffer, out var written) && written == KeySizeBytes
            ? buffer.AsSpan(0, written).ToArray()
            : null;
    }
}
