using System.Linq.Expressions;
using System.Reflection;
using Common.Application.Pagination;
using Common.Domain.Entities;
using Common.Domain.ResultMonad;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Persistence.Extensions;

public static class PaginationQueryableExtensions
{
    /// <summary>
    ///     Hybrid offset/keyset pagination.
    ///     <para>
    ///         Without <see cref="PaginationRequest.After" />: <c>OFFSET/LIMIT</c> exactly as before. When a
    ///         <paramref name="tiebreaker" /> is given the page also carries <see cref="PaginationResponse{T}.NextCursor" />
    ///         so a client can switch to keyset paging from any offset page.
    ///     </para>
    ///     <para>
    ///         With <see cref="PaginationRequest.After" />: the cursor's (sort value, tiebreaker) pair becomes a row-value
    ///         bound (<c>sort &lt; s OR (sort = s AND tie &gt; t)</c> for descending, <c>&gt;</c> for ascending) and the
    ///         page is <c>LIMIT</c> only, no <c>OFFSET</c>. Requires <paramref name="tiebreaker" /> (a unique, non-null
    ///         key) and does not combine with <paramref name="thenBy" />/<paramref name="thenByDescending" />: both
    ///         misuses throw <see cref="ArgumentException" /> at call time because they are programming errors, not
    ///         user input. A malformed cursor is user input and fails as a <c>Validation</c> error.
    ///     </para>
    ///     <para>
    ///         <see cref="PaginationRequest.IncludeTotal" /> false skips the count query and reports
    ///         <see cref="PaginationResponse{T}.TotalCount" /> <c>-1</c>.
    ///     </para>
    /// </summary>
    public static async Task<Result<PaginationResponse<TDto>>> PaginateAsync<TEntity, TDto>(
        this IQueryable<TEntity> queryable,
        Expression<Func<TEntity, TDto>> selector,
        PaginationRequest request,
        Expression<Func<TEntity, object>>? orderBy = null,
        Expression<Func<TEntity, object>>? orderByDescending = null,
        Expression<Func<TEntity, object>>? thenBy = null,
        Expression<Func<TEntity, object>>? thenByDescending = null,
        Expression<Func<TEntity, object>>? tiebreaker = null,
        CancellationToken cancellationToken = default)
        where TEntity : IAuditableEntity
    {
        var useCursor = request.After is not null;

        if (useCursor && tiebreaker is null)
        {
            throw new ArgumentException("Keyset pagination (After) requires a tiebreaker.", nameof(tiebreaker));
        }

        if (useCursor && (thenBy is not null || thenByDescending is not null))
        {
            throw new ArgumentException("Keyset pagination (After) supports one sort key plus the tiebreaker; thenBy is not supported.", nameof(thenBy));
        }

        // Primary sort key: the caller's, or the default CreatedOn descending.
        var descending = orderByDescending is not null || orderBy is null;
        Expression<Func<TEntity, object>> sortKey = orderByDescending ?? orderBy ?? (e => e.CreatedOn);

        var filtered = queryable;

        if (useCursor)
        {
            var decoded = PaginationCursor.Decode(request.After!);
            if (decoded.IsFailure)
            {
                return decoded.Error!;
            }

            // A well-formed cursor minted for another sort key type (the client changed the sort between pages)
            // is user input too: the same Validation failure as a malformed one, never an exception.
            if (KeysetPredicate(sortKey, tiebreaker!, decoded.Value!, descending) is not { } keyset)
            {
                return Error.Validation([PaginationCursor.ParameterName]);
            }

            filtered = filtered.Where(keyset);
        }

        var ordered = descending ? filtered.OrderByDescending(sortKey) : filtered.OrderBy(sortKey);

        if (thenByDescending is not null)
        {
            ordered = ordered.ThenByDescending(thenByDescending);
        }
        else if (thenBy is not null)
        {
            ordered = ordered.ThenBy(thenBy);
        }

        // Rows that tie on every key above still need one fixed order, or a page boundary can repeat or skip a row.
        // The caller supplies a unique key: IAuditableEntity has no id (composite-key entities exist).
        if (tiebreaker is not null)
        {
            ordered = ordered.ThenBy(tiebreaker);
        }

        var totalCount = request.ShouldIncludeTotal ? await queryable.CountAsync(cancellationToken) : -1;

        var page = useCursor ? ordered.Take(request.Take) : ordered.Skip(request.Skip).Take(request.Take);
        var pageNumber = useCursor ? 1 : request.PageNumber;

        if (tiebreaker is null)
        {
            var data = await page.Select(selector).ToListAsync(cancellationToken);
            return new PaginationResponse<TDto>(data, totalCount, pageNumber, request.PageSize);
        }

        // One query: the DTO plus the two keys of each row, so the last row yields the next cursor.
        var rows = await page.Select(KeyedRowSelector(selector, sortKey, tiebreaker)).ToListAsync(cancellationToken);
        var items = rows.Select(r => r.Item).ToList();
        var nextCursor = rows.Count == request.PageSize
            ? PaginationCursor.Encode(rows[^1].Sort, rows[^1].Tie)
            : null;

        return new PaginationResponse<TDto>(items, totalCount, pageNumber, request.PageSize, nextCursor);
    }

    /// <summary>
    ///     Row-value comparison spelled out as <c>sort &lt; s || (sort == s &amp;&amp; tie &gt; t)</c> so every provider
    ///     translates it. Null when the cursor's values do not fit the key types.
    /// </summary>
    private static Expression<Func<TEntity, bool>>? KeysetPredicate<TEntity>(
        Expression<Func<TEntity, object>> sortKey,
        Expression<Func<TEntity, object>> tiebreaker,
        PaginationCursor cursor,
        bool descending)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "e");
        var sort = Unbox(ReplaceParameter(sortKey.Body, sortKey.Parameters[0], parameter));
        var tie = Unbox(ReplaceParameter(tiebreaker.Body, tiebreaker.Parameters[0], parameter));

        if (!PaginationCursor.TryMaterialize(cursor.SortValue, sort.Type, out var sortValue)
            || !PaginationCursor.TryMaterialize(cursor.Tiebreaker, tie.Type, out var tieValue))
        {
            return null;
        }

        var sortBound = Bound(sortValue, sort.Type);
        var tieBound = Bound(tieValue, tie.Type);

        var sortMoved = descending ? LessThan(sort, sortBound) : GreaterThan(sort, sortBound);
        var sortEqual = Expression.Equal(sort, sortBound);
        // The tiebreaker is always ascending (ThenBy), whatever the primary direction.
        var tieMoved = GreaterThan(tie, tieBound);

        var body = Expression.OrElse(sortMoved, Expression.AndAlso(sortEqual, tieMoved));
        return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
    }

    private static Expression<Func<TEntity, KeyedRow<TDto>>> KeyedRowSelector<TEntity, TDto>(
        Expression<Func<TEntity, TDto>> selector,
        Expression<Func<TEntity, object>> sortKey,
        Expression<Func<TEntity, object>> tiebreaker)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "e");
        var item = ReplaceParameter(selector.Body, selector.Parameters[0], parameter);
        var sort = ReplaceParameter(sortKey.Body, sortKey.Parameters[0], parameter);
        var tie = ReplaceParameter(tiebreaker.Body, tiebreaker.Parameters[0], parameter);

        var constructor = typeof(KeyedRow<TDto>).GetConstructor([typeof(TDto), typeof(object), typeof(object)])!;
        var body = Expression.New(constructor, item, sort, tie);
        return Expression.Lambda<Func<TEntity, KeyedRow<TDto>>>(body, parameter);
    }

    /// <summary>A closure member access (not a bare constant) so EF parameterizes the bound instead of inlining it.</summary>
    private static MemberExpression Bound(object value, Type type)
    {
        var holderType = typeof(CursorBound<>).MakeGenericType(type);
        var holder = Activator.CreateInstance(holderType, value)!;
        return Expression.Property(Expression.Constant(holder, holderType), nameof(CursorBound<object>.Value));
    }

    /// <summary>
    ///     Strongly-typed ids, Guids and strings have no C# <c>&lt;</c> operator, so the node carries a marker method:
    ///     EF translates the node type to SQL and ignores the method; LINQ-to-objects runs the marker.
    /// </summary>
    private static BinaryExpression LessThan(Expression left, Expression right)
    {
        return HasNativeComparison(left.Type)
            ? Expression.LessThan(left, right)
            : Expression.LessThan(left, right, false, Marker(nameof(CompareLessThan), left.Type));
    }

    private static BinaryExpression GreaterThan(Expression left, Expression right)
    {
        return HasNativeComparison(left.Type)
            ? Expression.GreaterThan(left, right)
            : Expression.GreaterThan(left, right, false, Marker(nameof(CompareGreaterThan), left.Type));
    }

    private static bool HasNativeComparison(Type type)
    {
        var underlying = Nullable.GetUnderlyingType(type) ?? type;
        return underlying.IsPrimitive || underlying == typeof(decimal) || underlying == typeof(DateTime)
               || underlying == typeof(DateTimeOffset) || underlying == typeof(TimeSpan) || underlying == typeof(DateOnly)
               || underlying == typeof(TimeOnly);
    }

    private static MethodInfo Marker(string name, Type type)
    {
        return typeof(PaginationQueryableExtensions)
            .GetMethod(name, BindingFlags.Public | BindingFlags.Static)!
            .MakeGenericMethod(type);
    }

    /// <summary>Marker for <see cref="LessThan" />; public only so reflection can bind it without an accessibility bypass.</summary>
    public static bool CompareLessThan<T>(T left, T right)
    {
        return Comparer<T>.Default.Compare(left, right) < 0;
    }

    /// <summary>Marker for <see cref="GreaterThan" />; public only so reflection can bind it without an accessibility bypass.</summary>
    public static bool CompareGreaterThan<T>(T left, T right)
    {
        return Comparer<T>.Default.Compare(left, right) > 0;
    }

    /// <summary>Strips the boxing conversion an <c>Expression&lt;Func&lt;T, object&gt;&gt;</c> wraps value-typed keys in.</summary>
    private static Expression Unbox(Expression expression)
    {
        return expression is UnaryExpression { NodeType: ExpressionType.Convert or ExpressionType.ConvertChecked } unary
               && unary.Type == typeof(object)
            ? unary.Operand
            : expression;
    }

    private static Expression ReplaceParameter(Expression body, ParameterExpression from, ParameterExpression to)
    {
        return new ParameterReplacer(from, to).Visit(body);
    }

    private sealed class ParameterReplacer(ParameterExpression from, ParameterExpression to) : ExpressionVisitor
    {
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == from ? to : base.VisitParameter(node);
        }
    }

    private sealed class CursorBound<T>(T value)
    {
        public T Value { get; } = value;
    }

    private sealed class KeyedRow<TDto>(TDto item, object sort, object tie)
    {
        public TDto Item { get; } = item;
        public object Sort { get; } = sort;
        public object Tie { get; } = tie;
    }
}
