using Common.Domain.ResultMonad;

namespace Common.Application.Extensions;

public static class CollectionExtensions
{
    public static string JoinWithComma(this ICollection<string> collection)
    {
        return string.Join(",", collection);
    }

    public static Result<T> SingleAsResult<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
    {
        Result<T> result;
        try
        {
            var value = collection.SingleOrDefault(predicate);
            if (value is null)
            {
                result = Error.NotFound(nameof(T));
            }
            else
            {
                result = value!;
            }
        }
        catch (InvalidOperationException)
        {
            result = Result<T>.Failure(Error.ViolatesUniqueConstraint(nameof(T)));
        }

        return result;
    }

    public static Result<T> FirstAsResult<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
    {
        var value = collection.FirstOrDefault(predicate);
        if (value is null)
        {
            return Error.NotFound(nameof(T));
        }

        return value;
    }
}
