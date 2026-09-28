namespace Common.Application.Storage;

/// <summary>
///     Unsigned, publicly-readable objects meant to sit behind a CDN cache (e.g. product images, static assets).
///     Callers key objects by something stable (a content hash) so an object is never mutated in place: a changed
///     file is a new key, and the CDN cache never needs busting. There is deliberately no presigned-URL method
///     here: a public object is never signed, since presigned URLs defeat CDN caching (a distinct query string
///     per signature means every request misses cache); anything needing a presigned URL belongs in
///     <see cref="IPrivateObjectStore" /> instead.
/// </summary>
public interface IPublicObjectStore
{
    Task UploadAsync(UploadObjectRequest request, CancellationToken cancellationToken);

    /// <summary>Deterministic public URL: no signing, no network call.</summary>
    Uri GetPublicUrl(string key);

    Task<bool> ExistsAsync(string key, CancellationToken cancellationToken);

    /// <summary>Null when the key does not exist.</summary>
    Task<ObjectMetadata?> GetMetadataAsync(string key, CancellationToken cancellationToken);

    Task DeleteAsync(string key, CancellationToken cancellationToken);

    /// <summary>Deletes in S3 batch-delete requests of up to 1000 keys each.</summary>
    Task DeleteManyAsync(IReadOnlyCollection<string> keys, CancellationToken cancellationToken);

    /// <summary>Lists keys under <paramref name="prefix" />, newest S3 pagination (continuation token, not marker).</summary>
    Task<ObjectListPage> ListAsync(string prefix, string? continuationToken, int pageSize, CancellationToken cancellationToken);
}
