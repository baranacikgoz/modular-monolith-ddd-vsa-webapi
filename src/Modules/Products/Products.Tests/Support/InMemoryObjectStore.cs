using System.Collections.Concurrent;
using Common.Application.Storage;

namespace Products.Tests.Support;

/// <summary>
///     Dictionary-backed stand-in for both object stores, so the endpoints and runners are exercised without S3. The
///     factory registers one instance per interface, so a key written to the public store is not visible in the private one.
/// </summary>
public sealed class InMemoryObjectStore : IPublicObjectStore, IPrivateObjectStore
{
    private readonly ConcurrentDictionary<string, StoredObject> _objects = new(StringComparer.Ordinal);

    public IReadOnlyCollection<string> Keys => _objects.Keys.ToList();

    public void Clear() => _objects.Clear();

    public byte[] Read(string key) => _objects[key].Content;

    public string ContentTypeOf(string key) => _objects[key].ContentType;

    public async Task UploadAsync(UploadObjectRequest request, CancellationToken cancellationToken)
    {
        using var buffer = new MemoryStream();
        await request.Content.CopyToAsync(buffer, cancellationToken);
        _objects[request.Key] = new StoredObject(buffer.ToArray(), request.ContentType);
    }

    public Task<Stream> DownloadAsync(string key, CancellationToken cancellationToken)
    {
        return Task.FromResult<Stream>(new MemoryStream(_objects[key].Content, writable: false));
    }

    public Uri GetPublicUrl(string key) => new($"https://objects.test/{key}");

    public Task<bool> ExistsAsync(string key, CancellationToken cancellationToken) => Task.FromResult(_objects.ContainsKey(key));

    public Task<ObjectMetadata?> GetMetadataAsync(string key, CancellationToken cancellationToken)
    {
        return Task.FromResult(_objects.TryGetValue(key, out var stored)
            ? new ObjectMetadata(key, stored.ContentType, stored.Content.Length, null, null, new Dictionary<string, string>())
            : null);
    }

    public Task DeleteAsync(string key, CancellationToken cancellationToken)
    {
        _objects.TryRemove(key, out _);
        return Task.CompletedTask;
    }

    public Task DeleteManyAsync(IReadOnlyCollection<string> keys, CancellationToken cancellationToken)
    {
        foreach (var key in keys)
        {
            _objects.TryRemove(key, out _);
        }

        return Task.CompletedTask;
    }

    public Task<ObjectListPage> ListAsync(string prefix, string? continuationToken, int pageSize, CancellationToken cancellationToken)
    {
        var items = _objects
            .Where(o => o.Key.StartsWith(prefix, StringComparison.Ordinal))
            .Select(o => new ObjectListItem(o.Key, o.Value.Content.Length, null))
            .ToList();
        return Task.FromResult(new ObjectListPage(items, null));
    }

    public Uri CreatePresignedUploadUrl(string key, string contentType) => throw new NotSupportedException();

    public Uri CreatePresignedDownloadUrl(string key) => throw new NotSupportedException();

    private sealed record StoredObject(byte[] Content, string ContentType);
}
