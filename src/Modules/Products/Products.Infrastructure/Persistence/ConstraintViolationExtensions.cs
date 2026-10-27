using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Products.Infrastructure.Persistence;

public static class ConstraintViolationExtensions
{
    /// <summary>
    ///     Saves and turns the two constraint violations a product write can cause into results: a unique violation (a SKU
    ///     another writer took first) and a foreign key violation (the store or product template removed after it was read).
    ///     A check before the save answers the usual case; the database constraint is what is race-safe when two requests
    ///     pass that check together. Any other failure still throws.
    /// </summary>
    public static async Task<Result> SaveChangesDetectingConflictAsync(
        this IDbContext dbContext,
        Error uniqueViolationError,
        Error foreignKeyViolationError,
        CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success;
        }
        catch (DbUpdateException ex) when (ex.IsUniqueViolation())
        {
            return uniqueViolationError;
        }
        catch (DbUpdateException ex) when (ex.IsForeignKeyViolation())
        {
            return foreignKeyViolationError;
        }
    }

    public static bool IsUniqueViolation(this DbUpdateException exception)
    {
        return exception.InnerException is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation };
    }

    public static bool IsForeignKeyViolation(this DbUpdateException exception)
    {
        return exception.InnerException is PostgresException { SqlState: PostgresErrorCodes.ForeignKeyViolation };
    }
}
