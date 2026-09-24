using Microsoft.AspNetCore.Mvc;

namespace Common.Application.Pagination;

public abstract record PaginationRequest
{
    /// <summary>Offset page. Ignored when <see cref="After" /> is supplied.</summary>
    [FromQuery] public required int PageNumber { get; init; }

    [FromQuery] public required int PageSize { get; init; }

    /// <summary>
    ///     Opaque keyset cursor (<see cref="PaginationResponse{T}.NextCursor" /> of the previous page). When present the
    ///     page starts right after that row and <see cref="PageNumber" /> is ignored.
    /// </summary>
    [FromQuery] public string? After { get; init; }

    /// <summary>
    ///     False skips the <c>COUNT(*)</c> query: <see cref="PaginationResponse{T}.TotalCount" /> is then <c>-1</c>.
    ///     Nullable because minimal API binding treats a non-nullable value type as a required query parameter; absent
    ///     means true (<see cref="ShouldIncludeTotal" />).
    /// </summary>
    [FromQuery] public bool? IncludeTotal { get; init; }

    public bool ShouldIncludeTotal => IncludeTotal ?? true;

    public int Skip => (PageNumber - 1) * PageSize;
    public int Take => PageSize;
}
