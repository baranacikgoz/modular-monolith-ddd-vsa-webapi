using Common.Application.CQS;
using Common.Domain.ResultMonad;

namespace IAM.Application.OTP.Features.Send;

public sealed class SendOtpCommandHandler : ICommandHandler<SendOtpCommand>
{
    public async Task<Result> Handle(SendOtpCommand request, CancellationToken cancellationToken)
    {

        // Simulate sending sms
        await Task.Delay(300, cancellationToken);

        return Result.Success;
    }
}
