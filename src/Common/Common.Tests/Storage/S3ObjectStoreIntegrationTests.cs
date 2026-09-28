using System.Net.Http.Headers;
using System.Security.Cryptography;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Common.Application.Options;
using Common.Application.Storage;
using Common.Infrastructure.Storage;
using Microsoft.Extensions.Options;
using Testcontainers.LocalStack;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests.Storage;

/// <summary>
///     Exercises the real S3 wire protocol against LocalStack (AWS's own emulator), not a specific production
///     engine (deployment-chosen, endpoint-swappable): this proves ObjectStorage's client wiring (custom
///     ServiceURL, ForcePathStyle, multipart threshold, presigned URLs, batch delete, pagination) actually works
///     over HTTP, not just that it compiles. LocalStack images from 4.15 onward require a paid auth token (see
///     LocalStackBuilder.Validate), so this is pinned below that line.
/// </summary>
public sealed class S3ObjectStoreIntegrationTests : IAsyncLifetime
{
    private const string PublicBucket = "test-public-bucket";
    private const string PrivateBucket = "test-private-bucket";

    private readonly LocalStackContainer _container = new LocalStackBuilder("localstack/localstack:4.10").Build();
    private AmazonS3Client _s3Client = null!;
    private S3PublicObjectStore _publicStore = null!;
    private S3PrivateObjectStore _privateStore = null!;

    public async ValueTask InitializeAsync()
    {
        await _container.StartAsync();

        var options = new ObjectStorageOptions
        {
            ServiceUrl = _container.GetConnectionString(),
            Region = "us-east-1",
            AccessKey = "test",
            SecretKey = "test",
            ForcePathStyle = true,
            PublicBucketName = PublicBucket,
            PublicBaseUrl = "https://cdn.example.internal",
            PrivateBucketName = PrivateBucket,
            PresignedUploadExpirationMinutes = 15,
            PresignedDownloadExpirationMinutes = 15,
            MaxErrorRetry = 1,
            AttemptTimeoutSeconds = 30,
            MultipartThresholdMB = 1,
            CircuitBreakerFailureRatio = 0.9,
            CircuitBreakerMinimumThroughput = 100,
            CircuitBreakerSamplingDurationSeconds = 30,
            CircuitBreakerBreakDurationSeconds = 5
        };

        _s3Client = new AmazonS3Client(new BasicAWSCredentials(options.AccessKey, options.SecretKey), new AmazonS3Config
        {
            ServiceURL = options.ServiceUrl,
            ForcePathStyle = options.ForcePathStyle,
            AuthenticationRegion = options.Region
        });

        await _s3Client.PutBucketAsync(PublicBucket);
        await _s3Client.PutBucketAsync(PrivateBucket);

        var core = new S3ObjectStoreCore(_s3Client, options);
        _publicStore = new S3PublicObjectStore(core, Options.Create(options));
        _privateStore = new S3PrivateObjectStore(core, Options.Create(options), TimeProvider.System);
    }

    public async ValueTask DisposeAsync()
    {
        _s3Client.Dispose();
        await _container.DisposeAsync();
    }

    [Fact]
    public async Task PublicStore_UploadThenGetMetadataThenDelete_RoundTrips()
    {
        var key = $"catalog/{Guid.NewGuid()}/image.jpg";
        var content = "fake-image-bytes"u8.ToArray();

        await _publicStore.UploadAsync(
            new UploadObjectRequest(key, new MemoryStream(content), "image/jpeg"), CancellationToken.None);

        Assert.True(await _publicStore.ExistsAsync(key, CancellationToken.None));

        var metadata = await _publicStore.GetMetadataAsync(key, CancellationToken.None);
        Assert.NotNull(metadata);
        Assert.Equal("image/jpeg", metadata.ContentType);
        Assert.Equal(content.Length, metadata.ContentLength);

        await _publicStore.DeleteAsync(key, CancellationToken.None);

        Assert.False(await _publicStore.ExistsAsync(key, CancellationToken.None));
    }

    [Fact]
    public void PublicStore_GetPublicUrl_IsDeterministicAndUsesPublicBaseUrl()
    {
        var url = _publicStore.GetPublicUrl("catalog/abc123/image.jpg");

        Assert.Equal("https://cdn.example.internal/catalog/abc123/image.jpg", url.ToString());
    }

    [Fact]
    public async Task PublicStore_DeleteManyAsync_RemovesAllGivenKeys()
    {
        var keys = Enumerable.Range(0, 5).Select(i => $"bulk/{Guid.NewGuid()}-{i}.bin").ToList();

        foreach (var key in keys)
        {
            await _publicStore.UploadAsync(new UploadObjectRequest(key, new MemoryStream([1, 2, 3]), "application/octet-stream"),
                CancellationToken.None);
        }

        await _publicStore.DeleteManyAsync(keys, CancellationToken.None);

        foreach (var key in keys)
        {
            Assert.False(await _publicStore.ExistsAsync(key, CancellationToken.None));
        }
    }

    [Fact]
    public async Task PublicStore_ListAsync_PaginatesUnderPrefix()
    {
        var prefix = $"listing/{Guid.NewGuid()}/";
        var keys = Enumerable.Range(0, 3).Select(i => $"{prefix}item-{i}.bin").ToList();

        foreach (var key in keys)
        {
            await _publicStore.UploadAsync(new UploadObjectRequest(key, new MemoryStream([1]), "application/octet-stream"),
                CancellationToken.None);
        }

        var firstPage = await _publicStore.ListAsync(prefix, continuationToken: null, pageSize: 2, CancellationToken.None);
        Assert.Equal(2, firstPage.Items.Count);
        Assert.NotNull(firstPage.ContinuationToken);

        var secondPage = await _publicStore.ListAsync(prefix, firstPage.ContinuationToken, pageSize: 2, CancellationToken.None);
        Assert.Single(secondPage.Items);
        Assert.Null(secondPage.ContinuationToken);
    }

    [Fact]
    public async Task PublicStore_UploadAboveMultipartThreshold_SucceedsAndRoundTripsSize()
    {
        // MultipartThresholdMB is 1 in this fixture's options, so 2MB forces the multipart path in TransferUtility.
        var content = new byte[2 * 1024 * 1024];
        RandomNumberGenerator.Fill(content);
        var key = $"large/{Guid.NewGuid()}.bin";

        await _publicStore.UploadAsync(new UploadObjectRequest(key, new MemoryStream(content), "application/octet-stream"),
            CancellationToken.None);

        var metadata = await _publicStore.GetMetadataAsync(key, CancellationToken.None);
        Assert.NotNull(metadata);
        Assert.Equal(content.Length, metadata.ContentLength);
    }

    [Fact]
    public async Task PrivateStore_UploadThenDownload_ContentMatches()
    {
        var content = "invoice-pdf-bytes"u8.ToArray();
        var key = $"invoices/{Guid.NewGuid()}.pdf";

        await _privateStore.UploadAsync(new UploadObjectRequest(key, new MemoryStream(content), "application/pdf"),
            CancellationToken.None);

        await using var downloaded = await _privateStore.DownloadAsync(key, CancellationToken.None);
        using var buffer = new MemoryStream();
        await downloaded.CopyToAsync(buffer, CancellationToken.None);

        Assert.Equal(content, buffer.ToArray());
    }

    [Fact]
    public async Task PrivateStore_PresignedDownloadUrl_ActuallyDownloadsOverHttpWithoutOurApi()
    {
        var content = "report-bytes"u8.ToArray();
        var key = $"reports/{Guid.NewGuid()}.csv";

        await _privateStore.UploadAsync(new UploadObjectRequest(key, new MemoryStream(content), "text/csv"),
            CancellationToken.None);

        var presignedUrl = _privateStore.CreatePresignedDownloadUrl(key);

        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(presignedUrl);
        response.EnsureSuccessStatusCode();
        var downloaded = await response.Content.ReadAsByteArrayAsync();

        Assert.Equal(content, downloaded);
    }

    [Fact]
    public async Task PrivateStore_PresignedUploadUrl_ActuallyUploadsOverHttpWithoutOurApi()
    {
        var content = "direct-client-upload-bytes"u8.ToArray();
        var key = $"exports/{Guid.NewGuid()}.xlsx";

        var presignedUrl = _privateStore.CreatePresignedUploadUrl(key,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        using var httpClient = new HttpClient();
        using var requestContent = new ByteArrayContent(content);
        requestContent.Headers.ContentType =
            new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        var response = await httpClient.PutAsync(presignedUrl, requestContent);
        response.EnsureSuccessStatusCode();

        await using var downloaded = await _privateStore.DownloadAsync(key, CancellationToken.None);
        using var buffer = new MemoryStream();
        await downloaded.CopyToAsync(buffer, CancellationToken.None);

        Assert.Equal(content, buffer.ToArray());
    }

    [Fact]
    public async Task GetMetadataAsync_ForMissingKey_ReturnsNull()
    {
        var metadata = await _publicStore.GetMetadataAsync($"missing/{Guid.NewGuid()}.bin", CancellationToken.None);

        Assert.Null(metadata);
    }
}
