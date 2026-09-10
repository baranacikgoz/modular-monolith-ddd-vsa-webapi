using Microsoft.AspNetCore.Mvc;

namespace Common.Application.Extensions;

public static class ProblemDetailsExtensions
{
    /// <summary>
    ///     Always a field-keyed dictionary, the same shape ASP.NET's own HttpValidationProblemDetails uses for
    ///     FluentValidation failures, so a client never has to branch on error source to find a message.
    ///     <paramref name="parameterName" /> null or empty (an error with no single field, e.g. a state
    ///     conflict) lands under the empty-string key, matching ModelState's own convention for
    ///     non-field-specific errors.
    /// </summary>
    public static ProblemDetails AddErrors(this ProblemDetails problemDetails, string? parameterName,
        ICollection<string> errors)
    {
        problemDetails.Extensions.Add("errors", new Dictionary<string, string[]>
        {
            [parameterName ?? string.Empty] = [.. errors]
        });

        return problemDetails;
    }

    public static ProblemDetails AddErrorKey(this ProblemDetails problemDetails, string errorKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(errorKey);

        problemDetails.Extensions.Add("errorKey", errorKey);
        return problemDetails;
    }
}
