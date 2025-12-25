using Common.Domain.ResultMonad;

namespace IAM.Domain.Errors;

public static class IdentityErrors
{
    public static Error Some(ICollection<string> errors)
    {
        return new Error { Key = nameof(Some), SubErrors = errors };
    }
}
