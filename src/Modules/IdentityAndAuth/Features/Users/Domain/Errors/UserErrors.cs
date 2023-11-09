using System.Net;
using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Users.Domain.Errors;

internal static class UserErrors
{
    public static readonly Error NotFound = new(nameof(NotFound), HttpStatusCode.NotFound);
}
