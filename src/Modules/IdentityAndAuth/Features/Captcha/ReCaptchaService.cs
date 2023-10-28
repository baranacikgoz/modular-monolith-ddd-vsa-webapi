using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Common.Core.Contracts.Results;
using Common.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IdentityAndAuth.Features.Captcha;

public class ReCaptchaService(
    HttpClient httpClient,
    IOptions<CaptchaOptions> captchaOptionsProvider,
    ILogger<ReCaptchaService> logger
    ) : ICaptchaService
{
    private readonly CaptchaOptions _captchaOptions = captchaOptionsProvider.Value;
    public async Task<Result> ValidateAsync(string captchaToken, CancellationToken cancellationToken)
    {
        HttpResponseMessage? httpResponseMessage;
        using (var requestContent = GetRequestParameters(captchaToken, _captchaOptions.SecretKey))
        {
            httpResponseMessage = await httpClient.PostAsync(new Uri(_captchaOptions.CaptchaEndpoint), requestContent, cancellationToken);
        }

        if (httpResponseMessage is not { IsSuccessStatusCode: true } succeededResult)
        {
            logger.LogError("Captcha validation failed with status code {StatusCode}", httpResponseMessage.StatusCode);
            return CaptchaErrors.VerificationFailed;
        }

        var reCaptchaResponse = await succeededResult.Content.ReadFromJsonAsync<ReCaptchaResponse>(cancellationToken);

        if (reCaptchaResponse is not { Success: true })
        {
            logger.LogError("Captcha validation failed with response {Response}", reCaptchaResponse);
            return CaptchaErrors.VerificationFailed;
        }

        return Result.Success;
    }

    private static FormUrlEncodedContent GetRequestParameters(string captchaToken, string secretKey)
    {
        var parameters = new Dictionary<string, string>
        {
            ["secret"] = secretKey,
            ["response"] = captchaToken
        };

        return new FormUrlEncodedContent(parameters);
    }

    private sealed class ReCaptchaResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("challenge_ts")]
        public DateTime ChallengeTs { get; set; }

        [JsonPropertyName("hostname")]
        public string Hostname { get; set; } = default!;

        [JsonPropertyName("error-codes")]
        public string[] ErrorCodes { get; set; } = default!;
    }
}
