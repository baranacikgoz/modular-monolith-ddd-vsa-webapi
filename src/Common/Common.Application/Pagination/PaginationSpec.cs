using Ardalis.Specification;
using Common.Domain.Entities;

namespace Common.Application.Pagination;

public class PaginationSpec<T> : Specification<T>
    where T : class, IAuditableEntity
{
    public PaginationSpec(PaginationRequest paginationRequest)
    {
        PaginationRequest = paginationRequest;

        Query
            .Skip(paginationRequest.Skip)
            .Take(paginationRequest.PageSize)
            .OrderByDescending(x => x.CreatedOn);
    }

    public PaginationRequest PaginationRequest { get; }
}

public class PaginationSpec<T, TResult> : Specification<T, TResult>
    where T : class, IAuditableEntity
{
    public PaginationSpec(PaginationRequest paginationRequest)
    {
        PaginationRequest = paginationRequest;

        Query
            .Skip(paginationRequest.Skip)
            .Take(paginationRequest.PageSize)
            .OrderByDescending(x => x.CreatedOn);
    }

    public PaginationRequest PaginationRequest { get; }
}
