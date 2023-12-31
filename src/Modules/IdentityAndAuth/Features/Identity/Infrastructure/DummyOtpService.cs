﻿using Common.Core.Contracts.Results;
using IdentityAndAuth.Features.Identity.Domain;
using IdentityAndAuth.Features.Identity.Domain.Errors;

namespace IdentityAndAuth.Features.Identity.Infrastructure;

internal class DummyOtpService : IOtpService
{
    private const string DummyOtp = "123456";
    public Task<string> GetOtpAsync(string phoneNumber, CancellationToken cancellationToken) => Task.FromResult(DummyOtp);
    public async Task<Result> ValidateAsync(string otp, string phoneNumber, CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);

        if (otp == DummyOtp)
        {
            return Result.Success;
        }

        return OtpErrors.InvalidOtp;
    }
}
