using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Mvc;

namespace Common.Application.Extensions;

public static class ProblemDetailsExtensions
{
    public static ProblemDetails AddErrors(this ProblemDetails problemDetails, IEnumerable<string> errors)
    {
        if (errors is ICollection<string> { Count: 0 })
        {
            return problemDetails;
        }

        problemDetails.Extensions.Add("errors", errors);

        return problemDetails;
    }

    public static ProblemDetails AddErrorKey(this ProblemDetails problemDetails, Error error)
        => problemDetails.AddErrorKey(error.Key);
    public static ProblemDetails AddErrorKey(this ProblemDetails problemDetails, string errorKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(errorKey);

        problemDetails.Extensions.Add("errorKey", errorKey);
        return problemDetails;
    }
}
