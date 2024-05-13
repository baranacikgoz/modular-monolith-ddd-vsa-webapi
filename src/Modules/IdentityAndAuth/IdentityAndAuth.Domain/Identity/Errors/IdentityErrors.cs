using Common.Domain.ResultMonad;

namespace IdentityAndAuth.Domain.Identity.Errors;

public static class IdentityErrors
{
    public static Error Some(ICollection<string> errors) => new() { Key = nameof(Some), SubErrors = errors };
}
