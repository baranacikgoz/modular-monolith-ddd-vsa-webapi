using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

/// <summary>
///     S3-compatible object storage, provider-agnostic by design (any S3-compatible engine, self-hosted or not;
///     backend only needs an S3 API endpoint). See <see cref="PublicBucketName" />/<see cref="PrivateBucketName" />
///     for the two access patterns this is built around: unsigned/CDN-cacheable public objects versus
///     presigned-only private objects.
/// </summary>
public class ObjectStorageOptions
{
    /// <summary>S3 API endpoint, e.g. <c>https://storage.internal</c>.</summary>
    public required string ServiceUrl { get; set; }

    /// <summary>Most self-hosted S3-compatible engines still require a syntactically valid AWS region for SigV4
    /// signing even though they ignore its meaning; a placeholder like "us-east-1" is normal and permanent, not a
    /// pending decision.</summary>
    public required string Region { get; set; }

    public required string AccessKey { get; set; }

    public required string SecretKey { get; set; }

    /// <summary>True for virtual-hosted-style-incapable self-hosted engines (bucket in the path, not the host):
    /// almost always true outside real AWS.</summary>
    public required bool ForcePathStyle { get; set; }

    /// <summary>Unsigned, stable, content-hash-keyed reads meant to sit behind a CDN cache. Never presigned.</summary>
    public required string PublicBucketName { get; set; }

    /// <summary>Base URL clients read public objects from: a CDN or reverse-proxy hostname mapped to
    /// <see cref="PublicBucketName" />, not necessarily <see cref="ServiceUrl" /> itself. The storage engine's
    /// native S3 API surface should stay hidden behind a thin, read-only proxy; writes still go to
    /// <see cref="ServiceUrl" /> directly.</summary>
    public required string PublicBaseUrl { get; set; }

    /// <summary>Presigned-URL-only access (anything not meant to be publicly cacheable: generated documents,
    /// exports, user uploads). Never served unsigned.</summary>
    public required string PrivateBucketName { get; set; }

    public required int PresignedUploadExpirationMinutes { get; set; }

    public required int PresignedDownloadExpirationMinutes { get; set; }

    /// <summary>AWS SDK's own retry count for transient S3 errors (throttling, 5xx, network faults); the SDK
    /// already understands S3-specific retry semantics (e.g. SlowDown), so this is not layered under the generic
    /// HTTP resilience pipeline used for other third-party integrations (<see cref="ResiliencyOptions" />).</summary>
    public required int MaxErrorRetry { get; set; }

    public required int AttemptTimeoutSeconds { get; set; }

    /// <summary>Streams at or above this size upload via S3 multipart instead of a single PUT.</summary>
    public required int MultipartThresholdMB { get; set; }

    /// <summary>Circuit breaker guarding calls to the storage engine, independent from the SDK's own per-attempt
    /// retry: a sustained outage fails fast instead of every caller separately exhausting its own retries.</summary>
    public required double CircuitBreakerFailureRatio { get; set; }

    public required int CircuitBreakerMinimumThroughput { get; set; }

    public required int CircuitBreakerSamplingDurationSeconds { get; set; }

    public required int CircuitBreakerBreakDurationSeconds { get; set; }
}

public class ObjectStorageOptionsValidator : CustomValidator<ObjectStorageOptions>
{
    public ObjectStorageOptionsValidator()
    {
        RuleFor(o => o.ServiceUrl)
            .NotEmpty()
            .WithMessage("ServiceUrl must not be empty.")
            .Must(x => Uri.TryCreate(x, UriKind.Absolute, out _))
            .WithMessage("ServiceUrl must be a valid absolute URL.");

        RuleFor(o => o.Region)
            .NotEmpty()
            .WithMessage("Region must not be empty.");

        RuleFor(o => o.AccessKey)
            .NotEmpty()
            .WithMessage("AccessKey must not be empty.");

        RuleFor(o => o.SecretKey)
            .NotEmpty()
            .WithMessage("SecretKey must not be empty.");

        RuleFor(o => o.PublicBucketName)
            .NotEmpty()
            .WithMessage("PublicBucketName must not be empty.");

        RuleFor(o => o.PublicBaseUrl)
            .NotEmpty()
            .WithMessage("PublicBaseUrl must not be empty.")
            .Must(x => Uri.TryCreate(x, UriKind.Absolute, out _))
            .WithMessage("PublicBaseUrl must be a valid absolute URL.");

        RuleFor(o => o.PrivateBucketName)
            .NotEmpty()
            .WithMessage("PrivateBucketName must not be empty.")
            .NotEqual(o => o.PublicBucketName)
            .WithMessage("PrivateBucketName must be different from PublicBucketName: the public/private split is the whole point.");

        RuleFor(o => o.PresignedUploadExpirationMinutes)
            .GreaterThan(0)
            .WithMessage("PresignedUploadExpirationMinutes must be greater than 0.");

        RuleFor(o => o.PresignedDownloadExpirationMinutes)
            .GreaterThan(0)
            .WithMessage("PresignedDownloadExpirationMinutes must be greater than 0.");

        RuleFor(o => o.MaxErrorRetry)
            .GreaterThanOrEqualTo(0)
            .WithMessage("MaxErrorRetry must be greater than or equal to 0.");

        RuleFor(o => o.AttemptTimeoutSeconds)
            .GreaterThan(0)
            .WithMessage("AttemptTimeoutSeconds must be greater than 0.");

        RuleFor(o => o.MultipartThresholdMB)
            .GreaterThan(0)
            .WithMessage("MultipartThresholdMB must be greater than 0.");

        RuleFor(o => o.CircuitBreakerFailureRatio)
            .GreaterThan(0)
            .LessThanOrEqualTo(1)
            .WithMessage("CircuitBreakerFailureRatio must be between 0 (exclusive) and 1 (inclusive).");

        RuleFor(o => o.CircuitBreakerMinimumThroughput)
            .GreaterThanOrEqualTo(2)
            .WithMessage("CircuitBreakerMinimumThroughput must be at least 2.");

        RuleFor(o => o.CircuitBreakerSamplingDurationSeconds)
            .GreaterThan(0)
            .WithMessage("CircuitBreakerSamplingDurationSeconds must be greater than 0.");

        RuleFor(o => o.CircuitBreakerBreakDurationSeconds)
            .GreaterThan(0)
            .WithMessage("CircuitBreakerBreakDurationSeconds must be greater than 0.");
    }
}
