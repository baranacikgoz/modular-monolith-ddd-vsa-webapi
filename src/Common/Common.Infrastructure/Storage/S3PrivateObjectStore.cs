using Amazon.S3;
using Common.Application.Options;
using Common.Application.Storage;
using Microsoft.Extensions.Options;

namespace Common.Infrastructure.Storage;

internal sealed class S3PrivateObjectStore : IPrivateObjectStore
{
    private readonly S3ObjectStoreCore _core;
    private readonly string _bucket;
    private readonly TimeSpan _uploadExpiration;
    private readonly TimeSpan _downloadExpiration;
    private readonly TimeProvider _timeProvider;

    /// <summary>Owns its own <see cref="S3ObjectStoreCore" /> (and so its own circuit breaker); see
    /// <see cref="S3PublicObjectStore" />'s constructor doc for why this isn't shared.</summary>
    public S3PrivateObjectStore(IAmazonS3 s3Client, IOptions<ObjectStorageOptions> options, TimeProvider timeProvider)
    {
        _core = new S3ObjectStoreCore(s3Client, options.Value);
        _bucket = options.Value.PrivateBucketName;
        _uploadExpiration = TimeSpan.FromMinutes(options.Value.PresignedUploadExpirationMinutes);
        _downloadExpiration = TimeSpan.FromMinutes(options.Value.PresignedDownloadExpirationMinutes);
        _timeProvider = timeProvider;
    }

    public Task UploadAsync(UploadObjectRequest request, CancellationToken cancellationToken)
    {
        return _core.UploadAsync(_bucket, request, cancellationToken);
    }

    public Task<Stream> DownloadAsync(string key, CancellationToken cancellationToken)
    {
        return _core.DownloadAsync(_bucket, key, cancellationToken);
    }

    public Task<bool> ExistsAsync(string key, CancellationToken cancellationToken)
    {
        return _core.ExistsAsync(_bucket, key, cancellationToken);
    }

    public Task<ObjectMetadata?> GetMetadataAsync(string key, CancellationToken cancellationToken)
    {
        return _core.GetMetadataAsync(_bucket, key, cancellationToken);
    }

    public Task DeleteAsync(string key, CancellationToken cancellationToken)
    {
        return _core.DeleteAsync(_bucket, key, cancellationToken);
    }

    public Task DeleteManyAsync(IReadOnlyCollection<string> keys, CancellationToken cancellationToken)
    {
        return _core.DeleteManyAsync(_bucket, keys, cancellationToken);
    }

    public Task<ObjectListPage> ListAsync(string prefix, string? continuationToken, int pageSize, CancellationToken cancellationToken)
    {
        return _core.ListAsync(_bucket, prefix, continuationToken, pageSize, cancellationToken);
    }

    public Uri CreatePresignedUploadUrl(string key, string contentType)
    {
        return _core.CreatePresignedUrl(_bucket, key, HttpVerb.PUT, contentType, _timeProvider.GetUtcNow().UtcDateTime + _uploadExpiration);
    }

    public Uri CreatePresignedDownloadUrl(string key)
    {
        return _core.CreatePresignedUrl(_bucket, key, HttpVerb.GET, null, _timeProvider.GetUtcNow().UtcDateTime + _downloadExpiration);
    }
}
