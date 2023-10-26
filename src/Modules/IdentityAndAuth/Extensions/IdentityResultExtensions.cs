using Common.Core.Contracts.Results;
using Microsoft.AspNetCore.Identity;
using IdentityError = IdentityAndAuth.Identity.IdentityError;

namespace IdentityAndAuth.Extensions;

public static class IdentityResultExtensions
{
    public static Result ToResult(this IdentityResult identityResult)
    {
        return identityResult.Succeeded
            ? Result.Succeeded()
            : Result.Failed(identityResult.ToCustomIdentityError());
    }

    public static Result<T> ToResult<T>(this IdentityResult identityResult, T value)
    {
        return identityResult.Succeeded
            ? Result<T>.Succeeded(value)
            : Result<T>.Failed(identityResult.ToCustomIdentityError());
    }

    private static IdentityError ToCustomIdentityError(this IdentityResult identityResult)
    {
        var errors = identityResult.Errors.Select(e => e.Description);
        return new IdentityError(errors);
    }
}
