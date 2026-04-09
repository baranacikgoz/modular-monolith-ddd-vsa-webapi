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
    private readonly CaptchaOptions _captchaOptions = captchaOptionsProvider.Value;

    public string GetClientKey()
    {
        return _captchaOptions.ClientKey;
    }

    public async Task<Result> ValidateAsync(string captchaToken, CancellationToken cancellationToken)
    {
        HttpResponseMessage httpResponseMessage;
        using (var requestContent = GetRequestParameters(captchaToken, _captchaOptions.SecretKey))
        {
            // Use relative URI so the resilient HttpClient pipeline (retry, circuit breaker, timeout) is applied.
            // BaseAddress is configured in the HttpClient registration (Captcha/Setup.cs).
            httpResponseMessage = await httpClient.PostAsync(
                new Uri(_captchaOptions.CaptchaEndpoint, UriKind.RelativeOrAbsolute),
                requestContent,
                cancellationToken);
        }

        if (httpResponseMessage is not { IsSuccessStatusCode: true } succeededResult)
        {
            LoggerMessages.LogCaptchaValidationFailedWithStatusCode(logger, (int)httpResponseMessage.StatusCode);
            return CaptchaErrors.CaptchaServiceUnavailable;
        }

        ReCaptchaResponse? reCaptchaResponse;
        try
        {
            reCaptchaResponse = await succeededResult.Content.ReadFromJsonAsync<ReCaptchaResponse>(cancellationToken);
        }
        catch (JsonException ex)
        {
            LoggerMessages.LogCaptchaDeserializationFailed(logger, ex);
            return CaptchaErrors.CaptchaServiceUnavailable;
        }
        catch (NotSupportedException ex)
        {
            LoggerMessages.LogCaptchaDeserializationFailed(logger, ex);
            return CaptchaErrors.CaptchaServiceUnavailable;
        }

        if (reCaptchaResponse is not { Success: true })
        {
            LoggerMessages.LogCaptchaValidationFailedWithResponse(logger, reCaptchaResponse);
            return CaptchaErrors.NotHuman;
        }

        // reCAPTCHA v3 score validation: 1.0 = very likely human, 0.0 = very likely bot
        var scoreThreshold = _captchaOptions.ScoreThreshold > 0
            ? _captchaOptions.ScoreThreshold
            : DefaultScoreThreshold;

        if (reCaptchaResponse.Score < scoreThreshold)
        {
            LoggerMessages.LogCaptchaScoreBelowThreshold(logger, reCaptchaResponse.Score, scoreThreshold);
            return CaptchaErrors.NotHuman;
        }

        return Result.Success;
    }

    private static FormUrlEncodedContent GetRequestParameters(string captchaToken, string secretKey)
    {
        var parameters = new Dictionary<string, string> { ["secret"] = secretKey, ["response"] = captchaToken };

        return new FormUrlEncodedContent(parameters);
    }

    private static partial class LoggerMessages
    {
        [LoggerMessage(
            Level = LogLevel.Error,
            Message = "Captcha validation failed with status code {StatusCode}")]
        internal static partial void LogCaptchaValidationFailedWithStatusCode(ILogger logger, int statusCode);

        [LoggerMessage(
            Level = LogLevel.Error,
            Message = "Captcha validation failed with response {Response}")]
        internal static partial void LogCaptchaValidationFailedWithResponse(ILogger logger, ReCaptchaResponse? response);

        [LoggerMessage(
            Level = LogLevel.Error,
            Message = "Captcha response deserialization failed")]
        internal static partial void LogCaptchaDeserializationFailed(ILogger logger, Exception ex);

        [LoggerMessage(
            Level = LogLevel.Warning,
            Message = "Captcha score {Score} is below threshold {Threshold}")]
        internal static partial void LogCaptchaScoreBelowThreshold(ILogger logger, double score, double threshold);
    }

    internal sealed class ReCaptchaResponse
    {
        [JsonPropertyName("success")] public bool Success { get; set; }

        [JsonPropertyName("score")] public double Score { get; set; }

        [JsonPropertyName("challenge_ts")] public DateTime ChallengeTs { get; set; }

        [JsonPropertyName("hostname")] public string Hostname { get; set; } = default!;

        [JsonPropertyName("error-codes")] public string[] ErrorCodes { get; set; } = default!;

        public override string ToString() => $"Success: {Success}, Score: {Score}, Hostname: {Hostname}, Errors: {(ErrorCodes != null ? string.Join(", ", ErrorCodes) : "None")}";
    }
}
