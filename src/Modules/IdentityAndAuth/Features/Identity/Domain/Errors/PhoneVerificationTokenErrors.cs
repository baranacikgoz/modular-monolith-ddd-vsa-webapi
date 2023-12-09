using System.Net;
using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Identity.Domain.Errors;

internal static class PhoneVerificationTokenErrors
{
    public static readonly Error TokenNotFound = new(nameof(TokenNotFound), HttpStatusCode.NotFound);
    public static readonly Error NotMatching = new(nameof(NotMatching));
}
