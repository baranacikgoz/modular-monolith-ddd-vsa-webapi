using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Infrastructure.Persistence.Extensions;

/// <summary>
///     Index helpers for the two substring-search shapes a list screen needs on short identifier-like columns
///     (a name, a code, a barcode): <c>ILIKE '%term%'</c> served by a trigram GIN index, and <c>LIKE 'term%'</c>
///     served by a <c>text_pattern_ops</c> B-tree. Prose columns keep the full-text <c>tsvector</c> path
///     (<c>docs/full-text-search.md</c>); these are for fields where a user types a fragment of an exact value.
///     A DbContext that uses <see cref="HasTrigramIndex{TEntity}" /> must call <see cref="HasTrigramExtension" />
///     so the migration creates <c>pg_trgm</c> first.
/// </summary>
public static class TrigramIndexExtensions
{
    public const string TrigramExtensionName = "pg_trgm";
    private const string GinMethod = "gin";
    private const string TrigramOperatorClass = "gin_trgm_ops";
    private const string PatternOperatorClass = "text_pattern_ops";

    /// <summary>Declares the <c>pg_trgm</c> extension on the model; the next migration emits <c>CREATE EXTENSION IF NOT EXISTS</c>.</summary>
    public static ModelBuilder HasTrigramExtension(this ModelBuilder modelBuilder)
    {
        return modelBuilder.HasPostgresExtension(TrigramExtensionName);
    }

    /// <summary>GIN trigram index: accelerates <c>ILIKE '%term%'</c> and <c>LIKE '%term%'</c> on every listed column.</summary>
    public static IndexBuilder<TEntity> HasTrigramIndex<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, object?>> indexExpression)
        where TEntity : class
    {
        var index = builder.HasIndex(indexExpression).HasMethod(GinMethod);
        return index.HasOperators(OperatorsFor(index, TrigramOperatorClass));
    }

    /// <summary>B-tree with <c>text_pattern_ops</c>: accelerates anchored <c>LIKE 'term%'</c> regardless of collation.</summary>
    public static IndexBuilder<TEntity> HasPatternIndex<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, object?>> indexExpression)
        where TEntity : class
    {
        var index = builder.HasIndex(indexExpression);
        return index.HasOperators(OperatorsFor(index, PatternOperatorClass));
    }

    private static string[] OperatorsFor<TEntity>(IndexBuilder<TEntity> index, string operatorClass)
    {
        return [.. Enumerable.Repeat(operatorClass, index.Metadata.Properties.Count)];
    }
}
