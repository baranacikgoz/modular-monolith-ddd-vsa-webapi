using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Users.Services.PhoneVerificationToken;

internal sealed record PhoneVerificationTokenValidationFailedError()
    : Failure;
