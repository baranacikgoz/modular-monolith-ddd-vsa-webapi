using Common.Core.Contracts.Results;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Users.InitiatePhoneOwnershipProcess;

internal sealed class RequestHandler : IRequestHandler<Request, Result>
{
    public async ValueTask<Result> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        // Simulate sending sms otp.
        await Task.Delay(100, cancellationToken);

        return Result.Success;
    }
}
