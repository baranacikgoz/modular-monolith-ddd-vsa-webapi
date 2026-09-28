using System.Net;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Common.Application.Options;
using Common.Application.Storage;
using Polly;
using Polly.CircuitBreaker;

namespace Common.Infrastructure.Storage;

/// <summary>
///     S3 call logic shared by <see cref="S3PublicObjectStore" /> and <see cref="S3PrivateObjectStore" />: each
///     targets a different bucket and exposes a different interface (public objects are never presigned, private
///     objects are never served unsigned), but the underlying object operations are identical. Composition over a
///     shared base class because the two public interfaces intentionally do not share a common supertype: that
///     would let a caller request a presigned URL for the public bucket by accident.
///     Each store constructs its own instance of this class (never shared between the two): the circuit breaker
///     built in the constructor is therefore one per store, so a sustained outage on one bucket's calls fails
///     fast without also failing fast the other bucket's calls (Zero Trust: 3rd-party failures must not cascade).
///     Retries are left to the AWS SDK's own <see cref="ObjectStorageOptions.MaxErrorRetry" />, which already
///     understands S3-specific transient errors (throttling, 5xx); layering a second, generic retry on top would
///     double the backoff without adding correctness.
///     Callbacks take an explicit state tuple (the <c>(state, ct) =&gt;</c> shape already used by
///     <see cref="Common.Infrastructure.Resiliency.HttpClientKeyedExtensions" />) instead of a closure over
///     instance/local fields. During development, closures passed directly to <see cref="ResiliencePipeline" />
///     intermittently resolved against Polly's <c>ResilienceContext</c>-based sibling overload instead of the
///     intended <c>CancellationToken</c>-based one (surfacing as a body-level type error inside the lambda, not a
///     resolution-time ambiguity diagnostic), and this did not reproduce consistently across otherwise-similar
///     call sites in this file. The explicit-state shape resolved every case actually hit; treat it as the house
///     style for this file rather than a fully understood, restateable compiler rule.
/// </summary>
internal sealed class S3ObjectStoreCore
{
    private readonly IAmazonS3 _s3Client;
    private readonly TransferUtilityConfig _transferConfig;
    private readonly ResiliencePipeline _pipeline;
    private readonly string _serviceUrlScheme;

    public S3ObjectStoreCore(IAmazonS3 s3Client, ObjectStorageOptions options)
    {
        _s3Client = s3Client;
        _serviceUrlScheme = new Uri(options.ServiceUrl).Scheme;
        _transferConfig = new TransferUtilityConfig
        {
            MinSizeBeforePartUpload = options.MultipartThresholdMB * 1024L * 1024L
        };
        _pipeline = new ResiliencePipelineBuilder()
            .AddCircuitBreaker(new CircuitBreakerStrategyOptions
            {
                FailureRatio = options.CircuitBreakerFailureRatio,
                MinimumThroughput = options.CircuitBreakerMinimumThroughput,
                SamplingDuration = TimeSpan.FromSeconds(options.CircuitBreakerSamplingDurationSeconds),
                BreakDuration = TimeSpan.FromSeconds(options.CircuitBreakerBreakDurationSeconds),
                ShouldHandle = new PredicateBuilder()
                    .Handle<AmazonServiceException>(IsTransientS3Failure)
                    .Handle<HttpRequestException>()
                    .Handle<TaskCanceledException>()
            })
            .Build();
    }

    /// <summary>Server errors and throttling trip the breaker; client errors (404, 403, bad request) do not, since
    /// those are legitimate responses, not signs the dependency is unhealthy.</summary>
    private static bool IsTransientS3Failure(AmazonServiceException exception)
    {
        return (int)exception.StatusCode >= 500 || exception.StatusCode == HttpStatusCode.TooManyRequests;
    }

    public async Task UploadAsync(string bucket, UploadObjectRequest request, CancellationToken cancellationToken)
    {
        // A value-returning callback, not the void ExecuteAsync overload: a void async lambda here reproducibly
        // hit the ResilienceContext/CancellationToken mismatch described on this class's own doc comment, so every
        // callback in this file returns a (discarded, where irrelevant) result instead.
        await _pipeline.ExecuteAsync(
            static async (state, ct) =>
            {
                using var transferUtility = new TransferUtility(state.Client, state.TransferConfig);

                var uploadRequest = new TransferUtilityUploadRequest
                {
                    BucketName = state.Bucket,
                    Key = state.Request.Key,
                    InputStream = state.Request.Content,
                    ContentType = state.Request.ContentType,
                    AutoCloseStream = false // caller owns the stream's lifetime, not this store
                };

                foreach (var (metaKey, metaValue) in state.Request.Metadata ?? new Dictionary<string, string>())
                {
                    uploadRequest.Metadata.Add(metaKey, metaValue);
                }

                await transferUtility.UploadAsync(uploadRequest, ct);
                return true;
            },
            (Client: _s3Client, TransferConfig: _transferConfig, Bucket: bucket, Request: request),
            cancellationToken);
    }

    /// <summary>Caller must dispose the returned stream; it wraps the live HTTP response from the storage engine.</summary>
    public Task<Stream> DownloadAsync(string bucket, string key, CancellationToken cancellationToken)
    {
        return _pipeline.ExecuteAsync(
            static async (state, ct) =>
            {
                var response = await state.Client.GetObjectAsync(
                    new GetObjectRequest { BucketName = state.Bucket, Key = state.Key }, ct);
                return response.ResponseStream;
            },
            (Client: _s3Client, Bucket: bucket, Key: key),
            cancellationToken).AsTask();
    }

    public async Task<bool> ExistsAsync(string bucket, string key, CancellationToken cancellationToken)
    {
        return await GetMetadataAsync(bucket, key, cancellationToken) is not null;
    }

    public Task<ObjectMetadata?> GetMetadataAsync(string bucket, string key, CancellationToken cancellationToken)
    {
        return _pipeline.ExecuteAsync(
            static async (state, ct) =>
            {
                try
                {
                    var response = await state.Client.GetObjectMetadataAsync(
                        new GetObjectMetadataRequest { BucketName = state.Bucket, Key = state.Key }, ct);

                    var userMetadata = response.Metadata.Keys
                        .ToDictionary(k => k, k => response.Metadata[k], StringComparer.OrdinalIgnoreCase);

                    return new ObjectMetadata(state.Key, response.Headers.ContentType, response.ContentLength,
                        response.ETag, response.LastModified, userMetadata);
                }
                catch (AmazonS3Exception ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
            },
            (Client: _s3Client, Bucket: bucket, Key: key),
            cancellationToken).AsTask();
    }

    public Task DeleteAsync(string bucket, string key, CancellationToken cancellationToken)
    {
        return _pipeline.ExecuteAsync(
            static (state, ct) => new ValueTask(
                state.Client.DeleteObjectAsync(new DeleteObjectRequest { BucketName = state.Bucket, Key = state.Key }, ct)),
            (Client: _s3Client, Bucket: bucket, Key: key),
            cancellationToken).AsTask();
    }

    /// <summary>Batches into groups of 1000, the S3 DeleteObjects limit per call. S3 answers a batch delete with
    /// HTTP 200 even when individual keys within it failed (e.g. a lock/policy on one object), so each batch's
    /// <see cref="DeleteObjectsResponse.DeleteErrors" /> is checked explicitly rather than trusting a successful
    /// call to mean every key was removed.</summary>
    /// <exception cref="AmazonS3Exception">One or more keys in a batch failed to delete.</exception>
    public async Task DeleteManyAsync(string bucket, IReadOnlyCollection<string> keys, CancellationToken cancellationToken)
    {
        const int maxKeysPerBatch = 1000;

        foreach (var batch in keys.Chunk(maxKeysPerBatch))
        {
            var response = await _pipeline.ExecuteAsync(
                static (state, ct) =>
                {
                    var request = new DeleteObjectsRequest
                    {
                        BucketName = state.Bucket,
                        Objects = state.Batch.Select(k => new KeyVersion { Key = k }).ToList()
                    };
                    return new ValueTask<DeleteObjectsResponse>(state.Client.DeleteObjectsAsync(request, ct));
                },
                (Client: _s3Client, Bucket: bucket, Batch: batch),
                cancellationToken);

            // The SDK leaves DeleteErrors null, not an empty list, when nothing failed (no <Error> elements in the
            // response XML to deserialize into the collection).
            if (response.DeleteErrors is { Count: > 0 } deleteErrors)
            {
                var failures = string.Join("; ", deleteErrors.Select(e => $"{e.Key} ({e.Code}: {e.Message})"));
                throw new AmazonS3Exception($"Batch delete partially failed for bucket '{bucket}': {failures}");
            }
        }
    }

    public Task<ObjectListPage> ListAsync(
        string bucket, string prefix, string? continuationToken, int pageSize, CancellationToken cancellationToken)
    {
        return _pipeline.ExecuteAsync(
            static async (state, ct) =>
            {
                var response = await state.Client.ListObjectsV2Async(new ListObjectsV2Request
                {
                    BucketName = state.Bucket,
                    Prefix = state.Prefix,
                    ContinuationToken = state.ContinuationToken,
                    MaxKeys = state.PageSize
                }, ct);

                var items = response.S3Objects
                    .Select(o => new ObjectListItem(o.Key, o.Size ?? 0, o.LastModified))
                    .ToList();

                return new ObjectListPage(items, response.IsTruncated == true ? response.NextContinuationToken : null);
            },
            (Client: _s3Client, Bucket: bucket, Prefix: prefix, ContinuationToken: continuationToken, PageSize: pageSize),
            cancellationToken).AsTask();
    }

    public Uri CreatePresignedUrl(string bucket, string key, HttpVerb verb, string? contentType, DateTime expiresAtUtc)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = bucket,
            Key = key,
            Verb = verb,
            Expires = expiresAtUtc,
            Protocol = string.Equals(_serviceUrlScheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase) ? Protocol.HTTP : Protocol.HTTPS,
            ContentType = contentType
        };

        return new Uri(_s3Client.GetPreSignedURL(request));
    }
}
