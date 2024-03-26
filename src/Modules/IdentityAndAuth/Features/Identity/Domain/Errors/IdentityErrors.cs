using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Identity.Domain.Errors;

internal static class IdentityErrors
{
    public static Error Some(ICollection<string> errors) => new() { Key = nameof(Some), SubErrors = errors };
}
