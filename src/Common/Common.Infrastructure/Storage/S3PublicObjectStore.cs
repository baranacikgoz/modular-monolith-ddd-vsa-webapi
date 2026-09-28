using Common.Application.Options;
using Common.Application.Storage;
using Microsoft.Extensions.Options;

namespace Common.Infrastructure.Storage;

internal sealed class S3PublicObjectStore : IPublicObjectStore
{
    private readonly S3ObjectStoreCore _core;
    private readonly string _bucket;
    private readonly string _publicBaseUrl;

    public S3PublicObjectStore(S3ObjectStoreCore core, IOptions<ObjectStorageOptions> options)
    {
        _core = core;
        _bucket = options.Value.PublicBucketName;
        _publicBaseUrl = options.Value.PublicBaseUrl.TrimEnd('/');
    }

    public Task UploadAsync(UploadObjectRequest request, CancellationToken cancellationToken)
    {
        return _core.UploadAsync(_bucket, request, cancellationToken);
    }

    public Uri GetPublicUrl(string key)
    {
        var escapedSegments = key.Split('/').Select(Uri.EscapeDataString);
        return new Uri($"{_publicBaseUrl}/{string.Join('/', escapedSegments)}");
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
}
