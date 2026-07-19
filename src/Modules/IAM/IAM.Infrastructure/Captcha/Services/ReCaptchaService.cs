using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using IAM.Application.Captcha.Services;
using IAM.Domain.Captcha;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IAM.Infrastructure.Captcha.Services;

internal partial class ReCaptchaService(
    HttpClient httpClient,
    IOptions<CaptchaOptions> captchaOptionsProvider,
    ILogger<ReCaptchaService> logger
) : ICaptchaService
{
    private const double DefaultScoreThreshold = 0.5;

    public string GetClientKey()
    {
        return captchaOptionsProvider.Value.ClientKey;
    }

    public async Task<Result> ValidateAsync(string captchaToken, CancellationToken cancellationToken)
    {
        using var requestContent = GetRequestParameters(captchaToken, captchaOptionsProvider.Value.SecretKey);

        // A relative URI is used here so that the resilient HttpClient pipeline (retry, circuit
        // breaker, timeout) still applies. The base address is configured where this HttpClient
        // is registered, in the Captcha module setup.
        using var httpResponseMessage = await httpClient.PostAsync(
            new Uri(captchaOptionsProvider.Value.CaptchaEndpoint, UriKind.RelativeOrAbsolute),
            requestContent,
            cancellationToken);

        if (httpResponseMessage is not { IsSuccessStatusCode: true } succeededResult)
        {
            LogCaptchaValidationFailedWithStatusCode(logger, (int)httpResponseMessage.StatusCode);
            return CaptchaErrors.CaptchaServiceUnavailable;
        }

        ReCaptchaResponse? reCaptchaResponse;
        try
        {
            reCaptchaResponse = await succeededResult.Content.ReadFromJsonAsync<ReCaptchaResponse>(cancellationToken);
        }
        catch (JsonException ex)
        {
            LogCaptchaDeserializationFailed(logger, ex);
            return CaptchaErrors.CaptchaServiceUnavailable;
        }
        catch (NotSupportedException ex)
        {
            LogCaptchaDeserializationFailed(logger, ex);
            return CaptchaErrors.CaptchaServiceUnavailable;
        }

        if (reCaptchaResponse is not { Success: true })
        {
            LogCaptchaValidationFailedWithResponse(logger, reCaptchaResponse);
            return CaptchaErrors.NotHuman;
        }

        // reCAPTCHA v3 score validation: 1.0 = very likely human, 0.0 = very likely bot
        var scoreThreshold = captchaOptionsProvider.Value.ScoreThreshold > 0
            ? captchaOptionsProvider.Value.ScoreThreshold
            : DefaultScoreThreshold;

        if (reCaptchaResponse.Score < scoreThreshold)
        {
            LogCaptchaScoreBelowThreshold(logger, reCaptchaResponse.Score, scoreThreshold);
            return CaptchaErrors.NotHuman;
        }

        return Result.Success;
    }

    private static FormUrlEncodedContent GetRequestParameters(string captchaToken, string secretKey)
    {
        var parameters = new Dictionary<string, string> { ["secret"] = secretKey, ["response"] = captchaToken };

        return new FormUrlEncodedContent(parameters);
    }

    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "Captcha validation failed with status code {StatusCode}")]
    private static partial void LogCaptchaValidationFailedWithStatusCode(ILogger logger, int statusCode);

    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "Captcha validation failed with response {Response}")]
    private static partial void LogCaptchaValidationFailedWithResponse(ILogger logger, ReCaptchaResponse? response);

    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "Captcha response deserialization failed")]
    private static partial void LogCaptchaDeserializationFailed(ILogger logger, Exception ex);

    [LoggerMessage(
        Level = LogLevel.Warning,
        Message = "Captcha score {Score} is below threshold {Threshold}")]
    private static partial void LogCaptchaScoreBelowThreshold(ILogger logger, double score, double threshold);

    internal sealed class ReCaptchaResponse
    {
        [JsonPropertyName("success")] public bool Success { get; set; }

        [JsonPropertyName("score")] public double Score { get; set; }

        [JsonPropertyName("challenge_ts")] public DateTime ChallengeTs { get; set; }

        [JsonPropertyName("hostname")] public string Hostname { get; set; } = default!;

        [JsonPropertyName("error-codes")] public string[] ErrorCodes { get; set; } = default!;

        public override string ToString()
        {
            return
                $"Success: {Success}, Score: {Score}, Hostname: {Hostname}, Errors: {(ErrorCodes != null ? string.Join(", ", ErrorCodes) : "None")}";
        }
    }
}
