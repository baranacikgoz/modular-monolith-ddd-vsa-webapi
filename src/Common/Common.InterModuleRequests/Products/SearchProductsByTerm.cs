using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.Products;

/// <summary>
///     Free-text product search on behalf of another module's list filter. The result is cut at
///     <c>FullTextSearchOptions.MaxMatchResults</c>: an unbounded id list must not cross a module boundary. When
///     <see cref="SearchProductsByTermResponse.IsTruncated" /> is true the caller tells the user the match list was
///     cut instead of silently showing a partial answer.
/// </summary>
public sealed record SearchProductsByTermRequest(string Term) : IInterModuleRequest<SearchProductsByTermResponse>;

public sealed record SearchProductsByTermResponse(IReadOnlyCollection<DefaultIdType> ProductIds, bool IsTruncated);
