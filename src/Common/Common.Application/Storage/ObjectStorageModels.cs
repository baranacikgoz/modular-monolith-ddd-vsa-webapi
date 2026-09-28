namespace Common.Application.Storage;

/// <summary>One object to write. <paramref name="Content" /> is read to completion and not disposed by the store:
/// the caller owns its lifetime.</summary>
public sealed record UploadObjectRequest(
    string Key,
    Stream Content,
    string ContentType,
    IReadOnlyDictionary<string, string>? Metadata = null);

/// <summary>Result of a HEAD request: existence plus the metadata needed without downloading the object.</summary>
public sealed record ObjectMetadata(
    string Key,
    string ContentType,
    long ContentLength,
    string? ETag,
    DateTimeOffset? LastModified,
    IReadOnlyDictionary<string, string> UserMetadata);

public sealed record ObjectListItem(string Key, long ContentLength, DateTimeOffset? LastModified);

/// <summary>One page of a prefix listing. Pass <see cref="ContinuationToken" /> back in to fetch the next page;
/// null means this was the last page.</summary>
public sealed record ObjectListPage(IReadOnlyList<ObjectListItem> Items, string? ContinuationToken);
