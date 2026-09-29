using System.Net;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Common.Application.Options;
using Common.Infrastructure.Storage;
using NSubstitute;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests.Storage;

/// <summary>Mocked-<see cref="IAmazonS3" /> unit tests for behavior that's impractical to provoke against a real
/// S3-compatible server (e.g. a partial batch-delete failure, which needs object lock/versioning setup). Wire
/// protocol behavior itself is covered by <see cref="S3ObjectStoreIntegrationTests" /> against LocalStack.</summary>
public sealed class S3ObjectStoreCoreUnitTests
{
    private static ObjectStorageOptions ValidOptions() => new()
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
    public async Task DeleteManyAsync_WhenS3ReportsPartialFailure_ThrowsNamingTheFailedKey()
    {
        var s3Client = Substitute.For<IAmazonS3>();
        s3Client.DeleteObjectsAsync(Arg.Any<DeleteObjectsRequest>(), Arg.Any<CancellationToken>())
            .Returns(new DeleteObjectsResponse
            {
                DeleteErrors = [new DeleteError { Key = "bad-key", Code = "AccessDenied", Message = "denied" }]
            });

        var core = new S3ObjectStoreCore(s3Client, ValidOptions());

        var exception = await Assert.ThrowsAsync<AmazonS3Exception>(
            () => core.DeleteManyAsync("public-bucket", ["bad-key", "good-key"], CancellationToken.None));

        Assert.Contains("bad-key", exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public async Task DeleteManyAsync_WhenS3ReportsNoErrors_DoesNotThrow()
    {
        var s3Client = Substitute.For<IAmazonS3>();
        // The real SDK leaves DeleteErrors null (not an empty list) when nothing failed - this is the shape that
        // matters, an empty list would not have caught a null-check bug here.
        s3Client.DeleteObjectsAsync(Arg.Any<DeleteObjectsRequest>(), Arg.Any<CancellationToken>())
            .Returns(new DeleteObjectsResponse { DeleteErrors = null! });

        var core = new S3ObjectStoreCore(s3Client, ValidOptions());

        await core.DeleteManyAsync("public-bucket", ["key-one", "key-two"], CancellationToken.None);
    }

    [Fact]
    public async Task DeleteManyAsync_ChunksAccordingToConfiguredDeleteBatchSize()
    {
        var capturedBatchSizes = new List<int>();
        var s3Client = Substitute.For<IAmazonS3>();
        s3Client.DeleteObjectsAsync(Arg.Do<DeleteObjectsRequest>(r => capturedBatchSizes.Add(r.Objects.Count)), Arg.Any<CancellationToken>())
            .Returns(new DeleteObjectsResponse { DeleteErrors = null! });

        var options = ValidOptions();
        options.DeleteBatchSize = 2; // not the 1000 default: proves the batch size is read from Options, not hardcoded

        var core = new S3ObjectStoreCore(s3Client, options);

        await core.DeleteManyAsync("public-bucket", ["k1", "k2", "k3", "k4", "k5"], CancellationToken.None);

        Assert.Equal([2, 2, 1], capturedBatchSizes);
    }

    [Fact]
    public void IsTransientS3Failure_StatusAtOrAboveDefaultThreshold_IsTransient()
    {
        var core = new S3ObjectStoreCore(Substitute.For<IAmazonS3>(), ValidOptions());

        Assert.True(core.IsTransientS3Failure(new AmazonServiceException("boom") { StatusCode = HttpStatusCode.InternalServerError }));
    }

    [Fact]
    public void IsTransientS3Failure_StatusBelowConfiguredThreshold_IsNotTransient()
    {
        var options = ValidOptions();
        options.TransientErrorStatusCodeThreshold = 599; // raised past 500: proves the threshold is read from Options
        options.AdditionalTransientStatusCodes = [];
        var core = new S3ObjectStoreCore(Substitute.For<IAmazonS3>(), options);

        Assert.False(core.IsTransientS3Failure(new AmazonServiceException("boom") { StatusCode = HttpStatusCode.InternalServerError }));
    }

    [Fact]
    public void IsTransientS3Failure_ConfiguredAdditionalStatusCodeBelowThreshold_IsTransient()
    {
        var options = ValidOptions();
        options.AdditionalTransientStatusCodes = [418]; // arbitrary code below the threshold, added via config only
        var core = new S3ObjectStoreCore(Substitute.For<IAmazonS3>(), options);

        Assert.True(core.IsTransientS3Failure(new AmazonServiceException("boom") { StatusCode = (HttpStatusCode)418 }));
    }

    [Fact]
    public void IsTransientS3Failure_UnconfiguredClientError_IsNotTransient()
    {
        var core = new S3ObjectStoreCore(Substitute.For<IAmazonS3>(), ValidOptions());

        Assert.False(core.IsTransientS3Failure(new AmazonServiceException("boom") { StatusCode = HttpStatusCode.NotFound }));
    }
}
