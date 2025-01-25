using System.Linq.Expressions;
using Ardalis.Specification;
using Common.Domain.Entities;

namespace Common.Application.Queries.Pagination;

public class PaginationSpec<T> : Specification<T>
    where T : class, IAuditableEntity
{
    public PaginationSpec(PaginationQuery paginationQuery, Expression<Func<T, object?>>? orderExpression = null, bool orderByDesc = true)
    {
        PaginationQuery = paginationQuery;

        Query
            .Skip(paginationQuery.Skip)
            .Take(paginationQuery.PageSize);

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

    public PaginationQuery PaginationQuery { get; }
}

public class PaginationSpec<T, TResult> : Specification<T, TResult>
    where T : class, IAuditableEntity
{
    public PaginationSpec(PaginationQuery paginationQuery, Expression<Func<T, object?>>? orderExpression = null, bool orderByDesc = true)
    {
        PaginationQuery = paginationQuery;

        Query
            .Skip(paginationQuery.Skip)
            .Take(paginationQuery.PageSize);

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

    public PaginationQuery PaginationQuery { get; }
}
