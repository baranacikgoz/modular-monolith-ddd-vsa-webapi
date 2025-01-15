using System.Linq.Expressions;
using Ardalis.Specification;
using Common.Domain.Entities;

namespace Common.Application.Pagination;

public class PaginationSpec<T> : Specification<T>
    where T : class, IAuditableEntity
{
    public PaginationSpec(PaginationRequest paginationRequest, Expression<Func<T, object?>>? orderExpression = null, bool orderByDesc = true)
    {
        PaginationRequest = paginationRequest;

        Query
            .Skip(paginationRequest.Skip)
            .Take(paginationRequest.PageSize);

        if (orderExpression is not null)
        {
            if (orderByDesc)
            {
                Query.OrderByDescending(orderExpression);
            }
            else
            {
                Query.OrderBy(orderExpression);
            }
        }
        else
        {
            if (orderByDesc)
            {
                Query.OrderByDescending(x => x.CreatedOn);
            }
            else
            {
                Query.OrderBy(x => x.CreatedOn);
            }
        }
    }

    public PaginationRequest PaginationRequest { get; }
}

public class PaginationSpec<T, TResult> : Specification<T, TResult>
    where T : class, IAuditableEntity
{
    public PaginationSpec(PaginationRequest paginationRequest, Expression<Func<T, object?>>? orderExpression = null, bool orderByDesc = true)
    {
        PaginationRequest = paginationRequest;

        Query
            .Skip(paginationRequest.Skip)
            .Take(paginationRequest.PageSize);

        if (orderExpression is not null)
        {
            if (orderByDesc)
            {
                Query.OrderByDescending(orderExpression);
            }
            else
            {
                Query.OrderBy(orderExpression);
            }
        }
        else
        {
            if (orderByDesc)
            {
                Query.OrderByDescending(x => x.CreatedOn);
            }
            else
            {
                Query.OrderBy(x => x.CreatedOn);
            }
        }
    }

    public PaginationRequest PaginationRequest { get; }
}
