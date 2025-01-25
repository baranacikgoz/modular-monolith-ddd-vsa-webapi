using Common.Application.Caching;
using Common.Application.CQS;
using Common.Domain.ResultMonad;
using IAM.Domain.Errors;

namespace IAM.Application.OTP.Features.VerifyThenRemove;

public sealed class VerifyThenRemoveOtpCommandHandler(ICacheService cache) : ICommandHandler<VerifyThenRemoveOtpCommand>
{
    public async Task<Result> Handle(VerifyThenRemoveOtpCommand request, CancellationToken cancellationToken)
    {
        var cacheKey = OtpCacheKeys.GetKey(request.PhoneNumber);

        var otpFromCache = await cache.GetAsync<string>(cacheKey, cancellationToken);

        if (!string.Equals(otpFromCache, request.Otp, StringComparison.Ordinal))
        {
            return OtpErrors.InvalidOtp;
        }

        await cache.RemoveAsync(cacheKey, cancellationToken);

        return Result.Success;
    }
}
