using Common.Domain.ResultMonad;
using IAM.Application.Captcha.Services;
using IAM.Domain.Captcha;

namespace IAM.Infrastructure.Captcha.Services;

public class DummyCaptchaService : ICaptchaService
{
    public string GetClientKey()
    {
        const string dummyClientKey = "dummyClientKey";
        return dummyClientKey;
    }

    public async Task<Result> ValidateAsync(string captchaToken, CancellationToken cancellationToken)
    {
        const string dummyToken = "dummyToken";

        // Simulate api call
        await Task.Delay(100, cancellationToken);

        return string.Equals(captchaToken, dummyToken, StringComparison.Ordinal)
            ? Result.Success
            : Result.Failure(CaptchaErrors.NotHuman);
    }
}
