using Common.Core.Contracts.Results;
using IdentityAndAuth.Identity;
using IdentityAndAuth.Identity.Errors;
using Microsoft.AspNetCore.Identity;

namespace IdentityAndAuth.Extensions;

public static class IdentityResultExtensions
{
    public static Result ToResult(this IdentityResult identityResult)
    {
        return identityResult.Succeeded
            ? Result.Success
            : Result.Failure(identityResult.ToCustomIdentityError());
    }

    public static Result<T> ToResult<T>(this IdentityResult identityResult, T value)
    {
        return identityResult.Succeeded
            ? Result<T>.Success(value)
            : Result<T>.Failure(identityResult.ToCustomIdentityError());
    }

    private static Error ToCustomIdentityError(this IdentityResult identityResult)
    {
        var errors = identityResult.Errors.Select(e => e.Description);
        return new Error(nameof(IdentityErrors.Some), errors: errors);
    }
}
