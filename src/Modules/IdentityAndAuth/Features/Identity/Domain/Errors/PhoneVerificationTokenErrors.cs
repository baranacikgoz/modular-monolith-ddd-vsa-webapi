using System.Net;
using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Identity.Domain.Errors;

internal static class PhoneVerificationTokenErrors
{
    public static readonly Error PhoneVerificationTokenNotFound = new(nameof(PhoneVerificationTokenNotFound), statusCode: HttpStatusCode.NotFound);
    public static readonly Error PhoneVerificationTokensNotMatching = new(nameof(PhoneVerificationTokensNotMatching));
}
