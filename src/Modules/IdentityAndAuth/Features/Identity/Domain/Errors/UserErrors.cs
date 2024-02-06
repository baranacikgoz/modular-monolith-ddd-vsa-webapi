using System.Net;
using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Identity.Domain.Errors;

internal static class UserErrors
{
    public static readonly Error UserNotFound = new(nameof(UserNotFound), statusCode: HttpStatusCode.NotFound);
}
