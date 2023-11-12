﻿using Common.Core.Contracts.Results;
using IdentityAndAuth.Features.Identity.Domain;
using IdentityAndAuth.Features.Identity.Domain.Errors;

namespace IdentityAndAuth.Features.Identity.Infrastructure;

internal class DummyPhoneVerificationTokenService : IPhoneVerificationTokenService
{
    private const string DummyPhoneVerificationToken = "dummyPhoneVerificationToken";
    public Task<string> GetTokenAsync(string phoneNumber, CancellationToken cancellationToken)
        => Task.FromResult(DummyPhoneVerificationToken);

    public async Task<Result> ValidateTokenAsync(string phoneNumber, string token, CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);

        if (token != DummyPhoneVerificationToken)
        {
            return PhoneVerificationTokenErrors.VerificationFailed;
        }

        return Result.Success;
    }
}