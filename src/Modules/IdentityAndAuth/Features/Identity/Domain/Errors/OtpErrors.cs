using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Identity.Domain.Errors;

internal static class OtpErrors
{
    public static readonly Error InvalidOtp = new() { Key = nameof(InvalidOtp) };
}
