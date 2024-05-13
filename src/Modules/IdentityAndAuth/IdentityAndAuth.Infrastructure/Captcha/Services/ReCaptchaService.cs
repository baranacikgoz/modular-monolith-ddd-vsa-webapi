using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Options;
using IdentityAndAuth.Application.Captcha.Services;
using IdentityAndAuth.Domain.Captcha;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IdentityAndAuth.Application.Captcha.Infrastructure;

internal partial class ReCaptchaService(
    HttpClient httpClient,
    IOptions<CaptchaOptions> captchaOptionsProvider,
    ILogger<ReCaptchaService> logger
    ) : ICaptchaService
{
    private readonly CaptchaOptions _captchaOptions = captchaOptionsProvider.Value;
    public string GetClientKey() => _captchaOptions.ClientKey;
    public async Task<Result> ValidateAsync(string captchaToken, CancellationToken cancellationToken)
    {
        HttpResponseMessage? httpResponseMessage;
        using (var requestContent = GetRequestParameters(captchaToken, _captchaOptions.SecretKey))
        {
            httpResponseMessage = await httpClient.PostAsync(new Uri(_captchaOptions.CaptchaEndpoint), requestContent, cancellationToken);
        }

        if (httpResponseMessage is not { IsSuccessStatusCode: true } succeededResult)
        {
            LogCaptchaValidationFailedWithStatusCode(logger, (int)httpResponseMessage.StatusCode);
            return CaptchaErrors.CaptchaServiceUnavailable;
        }

        var reCaptchaResponse = await succeededResult.Content.ReadFromJsonAsync<ReCaptchaResponse>(cancellationToken);

        if (reCaptchaResponse is not { Success: true })
        {
            LogCaptchaValidationFailedWithResponse(logger, reCaptchaResponse);
            return CaptchaErrors.NotHuman;
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

    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "Captcha validation failed with status code {StatusCode}")]
    private static partial void LogCaptchaValidationFailedWithStatusCode(ILogger logger, int statusCode);

    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "Captcha validation failed with response {Response}")]
    private static partial void LogCaptchaValidationFailedWithResponse(ILogger logger, ReCaptchaResponse? response);
}
