using System.Net;
using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Identity;

public static class IdentityErrors
{
    public static Error Some(IEnumerable<string> errors) => new(nameof(Some), errors: errors);
}
