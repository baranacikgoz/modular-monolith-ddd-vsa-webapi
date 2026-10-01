using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using Amazon.Runtime;
using Amazon.S3;
using Common.Application.Options;
using Common.Application.Storage;
using Common.Infrastructure.Storage;
using Microsoft.Extensions.Options;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests.Storage;

/// <summary>
///     Captures the PUT the production client configuration (<c>Setup.CreateS3Config</c>) really sends over plain
///     HTTP. Self-hosted engines such as the SeaweedFS 3.x image the dev cluster runs store an <c>aws-chunked</c>
///     body as received (chunk headers and signatures included, <c>Content-Encoding: aws-chunked</c> kept as object
///     metadata), which makes every uploaded image unreadable in a browser. LocalStack
///     (<see cref="S3ObjectStoreIntegrationTests" />) decodes that framing itself, so only the wire format
///     catches it.
/// </summary>
public sealed class S3UploadWireFormatTests
{
    [Fact]
    public async Task PublicStore_UploadOverHttp_SendsTheFileBytesWithoutAwsChunkedFraming()
    {
        var port = GetFreeTcpPort();
        using var listener = new HttpListener();
        listener.Prefixes.Add($"http://127.0.0.1:{port}/");
        listener.Start();
#pragma warning disable CA2025 // awaited through Task.WhenAll below, before the listener's using scope ends
        var captured = CaptureFirstPutAsync(listener);
#pragma warning restore CA2025

        var options = CreateOptions($"http://127.0.0.1:{port}");
        using var s3Client = Common.Infrastructure.Storage.Setup.CreateS3Client(options);
        var store = new S3PublicObjectStore(s3Client, Options.Create(options));
        var content = new byte[64 * 1024 + 17];
        RandomNumberGenerator.Fill(content);

        var upload = store.UploadAsync(
            new UploadObjectRequest($"catalog/{Guid.NewGuid()}/image.jpg", new MemoryStream(content), "image/jpeg"),
            CancellationToken.None);
        await Task.WhenAll(captured, upload);

        var request = await captured;
        Assert.DoesNotContain("aws-chunked", request.ContentEncoding ?? string.Empty, StringComparison.Ordinal);
        Assert.DoesNotContain("STREAMING", request.ContentSha256 ?? string.Empty, StringComparison.Ordinal);
        Assert.Equal(content, request.Body);
    }

    private static async Task<CapturedPut> CaptureFirstPutAsync(HttpListener listener)
    {
        var context = await listener.GetContextAsync();
        using var buffer = new MemoryStream();
        await context.Request.InputStream.CopyToAsync(buffer);

        var captured = new CapturedPut(
            context.Request.Headers["Content-Encoding"],
            context.Request.Headers["x-amz-content-sha256"],
            buffer.ToArray());

        context.Response.StatusCode = (int)HttpStatusCode.OK;
        context.Response.Headers["ETag"] = "\"d41d8cd98f00b204e9800998ecf8427e\"";
        context.Response.Close();
        return captured;
    }

    private static int GetFreeTcpPort()
    {
        using var probe = new TcpListener(IPAddress.Loopback, 0);
        probe.Start();
        return ((IPEndPoint)probe.LocalEndpoint).Port;
    }

    private static ObjectStorageOptions CreateOptions(string serviceUrl)
    {
        return new ObjectStorageOptions
        {
            ServiceUrl = serviceUrl,
            Region = "us-east-1",
            AccessKey = "test",
            SecretKey = "test",
            ForcePathStyle = true,
            PublicBucketName = "test-public-bucket",
            PublicBaseUrl = "https://cdn.example.internal",
            PrivateBucketName = "test-private-bucket",
            PresignedUploadExpirationMinutes = 15,
            PresignedDownloadExpirationMinutes = 15,
            MaxErrorRetry = 0,
            RetryMode = ObjectStorageRetryMode.Standard,
            AttemptTimeoutSeconds = 30,
            MultipartThresholdMB = 16,
            CircuitBreakerFailureRatio = 0.9,
            CircuitBreakerMinimumThroughput = 100,
            CircuitBreakerSamplingDurationSeconds = 30,
            CircuitBreakerBreakDurationSeconds = 5,
            DeleteBatchSize = 1000,
            TransientErrorStatusCodeThreshold = 500,
            AdditionalTransientStatusCodes = [429]
        };
    }

    private sealed record CapturedPut(string? ContentEncoding, string? ContentSha256, byte[] Body);
}
