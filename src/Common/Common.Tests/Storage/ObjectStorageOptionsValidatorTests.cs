using Common.Application.Options;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests.Storage;

public sealed class ObjectStorageOptionsValidatorTests
{
    private static ObjectStorageOptions Valid() => new()
    {
        ServiceUrl = "https://storage.internal",
        Region = "us-east-1",
        AccessKey = "access-key",
        SecretKey = "secret-key",
        ForcePathStyle = true,
        PublicBucketName = "public-bucket",
        PublicBaseUrl = "https://cdn.example.internal",
        PrivateBucketName = "private-bucket",
        PresignedUploadExpirationMinutes = 15,
        PresignedDownloadExpirationMinutes = 15,
        MaxErrorRetry = 3,
        RetryMode = ObjectStorageRetryMode.Standard,
        AttemptTimeoutSeconds = 30,
        MultipartThresholdMB = 16,
        CircuitBreakerFailureRatio = 0.5,
        CircuitBreakerMinimumThroughput = 5,
        CircuitBreakerSamplingDurationSeconds = 30,
        CircuitBreakerBreakDurationSeconds = 30,
        DeleteBatchSize = 1000,
        TransientErrorStatusCodeThreshold = 500,
        AdditionalTransientStatusCodes = [429]
    };

    [Fact]
    public void ValidOptions_PassesValidation()
    {
        var result = new ObjectStorageOptionsValidator().Validate(Valid());

        Assert.True(result.IsValid);
    }

    [Fact]
    public void RetryMode_AdaptiveIsAlsoValid()
    {
        var options = Valid();
        options.RetryMode = ObjectStorageRetryMode.Adaptive;

        var result = new ObjectStorageOptionsValidator().Validate(options);

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData("not-a-url")]
    [InlineData("storage.internal")]
    public void ServiceUrl_MustBeAbsoluteUrl(string value)
    {
        var options = Valid();
        options.ServiceUrl = value;

        var result = new ObjectStorageOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(ObjectStorageOptions.ServiceUrl));
    }

    [Theory]
    [InlineData("")]
    [InlineData("not-a-url")]
    public void PublicBaseUrl_MustBeAbsoluteUrl(string value)
    {
        var options = Valid();
        options.PublicBaseUrl = value;

        var result = new ObjectStorageOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(ObjectStorageOptions.PublicBaseUrl));
    }

    [Fact]
    public void PrivateBucketName_MustDifferFromPublicBucketName()
    {
        var options = Valid();
        options.PrivateBucketName = options.PublicBucketName;

        var result = new ObjectStorageOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(ObjectStorageOptions.PrivateBucketName));
    }

    [Fact]
    public void ForcePathStyle_MustBeSet()
    {
        var options = Valid();
        options.ForcePathStyle = null;

        var result = new ObjectStorageOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(ObjectStorageOptions.ForcePathStyle));
    }

    [Fact]
    public void MaxErrorRetry_MustBeSet()
    {
        var options = Valid();
        options.MaxErrorRetry = null;

        var result = new ObjectStorageOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(ObjectStorageOptions.MaxErrorRetry));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void PresignedUploadExpirationMinutes_MustBeGreaterThanZero(int value)
    {
        var options = Valid();
        options.PresignedUploadExpirationMinutes = value;

        var result = new ObjectStorageOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(ObjectStorageOptions.PresignedUploadExpirationMinutes));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1.5)]
    public void CircuitBreakerFailureRatio_MustBeBetweenZeroExclusiveAndOneInclusive(double value)
    {
        var options = Valid();
        options.CircuitBreakerFailureRatio = value;

        var result = new ObjectStorageOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(ObjectStorageOptions.CircuitBreakerFailureRatio));
    }

    [Fact]
    public void MaxErrorRetry_ZeroIsAllowed()
    {
        var options = Valid();
        options.MaxErrorRetry = 0;

        var result = new ObjectStorageOptionsValidator().Validate(options);

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1001)]
    public void DeleteBatchSize_MustBeBetweenOneAndOneThousand(int value)
    {
        var options = Valid();
        options.DeleteBatchSize = value;

        var result = new ObjectStorageOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(ObjectStorageOptions.DeleteBatchSize));
    }

    [Theory]
    [InlineData(399)]
    [InlineData(600)]
    public void TransientErrorStatusCodeThreshold_MustBeAValidHttpStatusCodeRange(int value)
    {
        var options = Valid();
        options.TransientErrorStatusCodeThreshold = value;

        var result = new ObjectStorageOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(ObjectStorageOptions.TransientErrorStatusCodeThreshold));
    }

    [Fact]
    public void AdditionalTransientStatusCodes_MustNotBeNull()
    {
        var options = Valid();
        options.AdditionalTransientStatusCodes = null!;

        var result = new ObjectStorageOptionsValidator().Validate(options);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(ObjectStorageOptions.AdditionalTransientStatusCodes));
    }
}
