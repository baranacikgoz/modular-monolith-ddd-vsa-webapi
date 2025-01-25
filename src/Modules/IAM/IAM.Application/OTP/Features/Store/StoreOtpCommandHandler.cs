using Common.Application.Caching;
using Common.Application.CQS;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace IAM.Application.OTP.Features.Store;

public sealed class StoreOtpCommandHandler(ICacheService cache, IOptions<OtpOptions> otpOptionsProvider) : ICommandHandler<StoreOtpCommand>
{
    public async Task<Result> Handle(StoreOtpCommand request, CancellationToken cancellationToken)
    {
        var cacheKey = OtpCacheKeys.GetKey(request.PhoneNumber);
        var expirationInMinutes = otpOptionsProvider.Value.ExpirationInMinutes;

        await cache
             .SetAsync(
                key: cacheKey,
                value: request.Otp,
                absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(expirationInMinutes),
                cancellationToken: cancellationToken);

        return Result.Success;
    }
}
