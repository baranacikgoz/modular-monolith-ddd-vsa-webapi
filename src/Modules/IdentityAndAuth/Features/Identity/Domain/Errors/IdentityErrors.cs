using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Identity.Domain.Errors;

internal static class IdentityErrors
{
    public static Error Some(IEnumerable<string> errors) => new() { Key = nameof(Some), SubErrors = errors };
}
