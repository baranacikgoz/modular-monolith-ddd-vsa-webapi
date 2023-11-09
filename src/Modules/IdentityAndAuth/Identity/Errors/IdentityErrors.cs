using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Identity.Errors;

internal static class IdentityErrors
{
    public static Error Some(IEnumerable<string> errors) => new(nameof(Some), errors: errors);
}
