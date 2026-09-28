namespace Common.Application.Storage;

/// <summary>
///     Private objects (generated documents, exports, user uploads, anything not meant to be publicly cacheable):
///     reachable only through a presigned URL or a server-side call, never unsigned. Presigned URLs are the right
///     tool for this class of object, not a workaround, since they deliberately sit off the public CDN-cached
///     read path <see cref="IPublicObjectStore" /> is built for.
/// </summary>
public interface IPrivateObjectStore
{
    /// <summary>Server-side upload, for content the backend itself produces (a generated report, an export).</summary>
    Task UploadAsync(UploadObjectRequest request, CancellationToken cancellationToken);

    Task<Stream> DownloadAsync(string key, CancellationToken cancellationToken);

    Task<bool> ExistsAsync(string key, CancellationToken cancellationToken);

    /// <summary>Null when the key does not exist.</summary>
    Task<ObjectMetadata?> GetMetadataAsync(string key, CancellationToken cancellationToken);

    Task DeleteAsync(string key, CancellationToken cancellationToken);

    /// <summary>Deletes in S3 batch-delete requests of up to 1000 keys each.</summary>
    Task DeleteManyAsync(IReadOnlyCollection<string> keys, CancellationToken cancellationToken);

    /// <summary>Lists keys under <paramref name="prefix" />, newest S3 pagination (continuation token, not marker).</summary>
    Task<ObjectListPage> ListAsync(string prefix, string? continuationToken, int pageSize, CancellationToken cancellationToken);

    /// <summary>A client (browser, mobile, another service) uploads directly to storage; the bytes never pass
    /// through our API.</summary>
    Uri CreatePresignedUploadUrl(string key, string contentType);

    /// <summary>A client downloads directly from storage; the bytes never pass through our API.</summary>
    Uri CreatePresignedDownloadUrl(string key);
}
