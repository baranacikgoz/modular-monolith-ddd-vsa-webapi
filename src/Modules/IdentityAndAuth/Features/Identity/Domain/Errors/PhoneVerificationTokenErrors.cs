using System.Net;
using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Identity.Domain.Errors;

internal static class PhoneVerificationTokenErrors
{
    public static readonly Error PhoneVerificationTokensNotMatching = new() { Key = nameof(PhoneVerificationTokensNotMatching) };
}
