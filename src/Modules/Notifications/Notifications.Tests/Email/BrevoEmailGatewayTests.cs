using System.Diagnostics.Metrics;
using System.Net;
using System.Text;
using System.Text.Json;
using Common.Application.Options;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Notifications.Application.Email;
using Notifications.Infrastructure.Email.Brevo;
using Notifications.Infrastructure.Telemetry;
using Xunit;

namespace Notifications.Tests.Email;

public sealed class BrevoEmailGatewayTests
{
    private const string To = "someone@example.com";
    private const long MaxResponseContentBufferBytes = 64 * 1024;

    private static EmailMessage Message(string? textBody = "plain 123456") =>
        new(To, "Your code", "<p>123456</p>", textBody);

    private static TestContext BuildSut(Func<HttpRequestMessage, string, HttpResponseMessage> respond) => new(respond);

    [Fact]
    public async Task SendAsync_Created_ReturnsSuccess()
    {
        using var ctx = BuildSut((_, _) => JsonResponse(HttpStatusCode.Created, """{"messageId":"<abc@smtp-relay.mailin.fr>"}"""));

        var result = await ctx.Gateway.SendAsync(Message(), CancellationToken.None);

        Assert.False(result.IsFailure);
    }

    [Fact]
    public async Task SendAsync_Success_RequestHasExpectedShape()
    {
        HttpRequestMessage? capturedRequest = null;
        string? capturedBody = null;
        using var ctx = BuildSut((request, body) =>
        {
            capturedRequest = request;
            capturedBody = body;
            return JsonResponse(HttpStatusCode.Created, """{"messageId":"1"}""");
        });

        await ctx.Gateway.SendAsync(Message(), CancellationToken.None);

        Assert.NotNull(capturedRequest);
        Assert.Equal(HttpMethod.Post, capturedRequest.Method);
        Assert.Equal("https://api.brevo.com/v3/smtp/email", capturedRequest.RequestUri!.ToString());
        using var doc = JsonDocument.Parse(capturedBody!);
        Assert.Equal("no-reply@example.com", doc.RootElement.GetProperty("sender").GetProperty("email").GetString());
        Assert.Equal("Sender", doc.RootElement.GetProperty("sender").GetProperty("name").GetString());
        Assert.Equal(To, doc.RootElement.GetProperty("to")[0].GetProperty("email").GetString());
        Assert.Equal("Your code", doc.RootElement.GetProperty("subject").GetString());
        Assert.Equal("<p>123456</p>", doc.RootElement.GetProperty("htmlContent").GetString());
        Assert.Equal("plain 123456", doc.RootElement.GetProperty("textContent").GetString());
    }

    [Fact]
    public async Task SendAsync_NoTextBody_OmitsTextContent()
    {
        string? capturedBody = null;
        using var ctx = BuildSut((_, body) =>
        {
            capturedBody = body;
            return JsonResponse(HttpStatusCode.Created, """{"messageId":"1"}""");
        });

        await ctx.Gateway.SendAsync(Message(textBody: null), CancellationToken.None);

        using var doc = JsonDocument.Parse(capturedBody!);
        Assert.False(doc.RootElement.TryGetProperty("textContent", out _));
    }

    [Fact]
    public async Task SendAsync_SuccessWithUnparseableBody_StillReturnsSuccess()
    {
        using var ctx = BuildSut((_, _) => JsonResponse(HttpStatusCode.Created, "not json"));

        var result = await ctx.Gateway.SendAsync(Message(), CancellationToken.None);

        Assert.False(result.IsFailure);
    }

    [Theory]
    [InlineData(HttpStatusCode.BadRequest)]
    [InlineData(HttpStatusCode.UnprocessableEntity)]
    [InlineData(HttpStatusCode.Unauthorized)]
    [InlineData(HttpStatusCode.Forbidden)]
    public async Task SendAsync_RejectedStatuses_ReturnsRejected(HttpStatusCode statusCode)
    {
        using var ctx = BuildSut((_, _) => JsonResponse(statusCode, """{"code":"invalid_parameter","message":"bad"}"""));

        var result = await ctx.Gateway.SendAsync(Message(), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(EmailErrors.Rejected.Key, result.Error!.Key);
    }

    [Fact]
    public async Task SendAsync_TooManyRequests_ReturnsThrottled()
    {
        using var ctx = BuildSut((_, _) => JsonResponse(HttpStatusCode.TooManyRequests, """{"code":"too_many_requests","message":"slow down"}"""));

        var result = await ctx.Gateway.SendAsync(Message(), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(EmailErrors.Throttled.Key, result.Error!.Key);
    }

    [Theory]
    [InlineData(HttpStatusCode.InternalServerError)]
    [InlineData(HttpStatusCode.BadGateway)]
    [InlineData(HttpStatusCode.ServiceUnavailable)]
    public async Task SendAsync_ServerErrors_ReturnsProviderUnavailable(HttpStatusCode statusCode)
    {
        using var ctx = BuildSut((_, _) => JsonResponse(statusCode, """{"code":"internal","message":"boom"}"""));

        var result = await ctx.Gateway.SendAsync(Message(), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(EmailErrors.ProviderUnavailable.Key, result.Error!.Key);
    }

    [Fact]
    public async Task SendAsync_ErrorWithMalformedBody_ReturnsProviderUnavailable()
    {
        using var ctx = BuildSut((_, _) => JsonResponse(HttpStatusCode.BadRequest, "<html>nope</html>"));

        var result = await ctx.Gateway.SendAsync(Message(), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(EmailErrors.ProviderUnavailable.Key, result.Error!.Key);
    }

    [Fact]
    public async Task SendAsync_ThrowsHttpRequestException_ReturnsProviderUnavailable()
    {
        using var ctx = BuildSut((_, _) => throw new HttpRequestException("connection reset"));

        var result = await ctx.Gateway.SendAsync(Message(), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(EmailErrors.ProviderUnavailable.Key, result.Error!.Key);
    }

    [Fact]
    public async Task SendAsync_TimesOutWithoutCallerCancellation_ReturnsProviderUnavailable()
    {
        using var ctx = BuildSut((_, _) => throw new TaskCanceledException("attempt timeout"));

        var result = await ctx.Gateway.SendAsync(Message(), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(EmailErrors.ProviderUnavailable.Key, result.Error!.Key);
    }

    [Fact]
    public async Task SendAsync_CallerCancelsBeforeCall_PropagatesCancellation()
    {
        using var ctx = BuildSut((_, _) => JsonResponse(HttpStatusCode.Created, """{"messageId":"1"}"""));
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => ctx.Gateway.SendAsync(Message(), cts.Token));
    }

    [Fact]
    public async Task SendAsync_ResponseExceedsMaxBufferSize_ReturnsProviderUnavailableWithoutThrowing()
    {
        var oversizedJson = $$"""{"messageId":"{{new string('x', (int)MaxResponseContentBufferBytes + 1024)}}"}""";
        using var ctx = BuildSut((_, _) => JsonResponse(HttpStatusCode.Created, oversizedJson));

        var result = await ctx.Gateway.SendAsync(Message(), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(EmailErrors.ProviderUnavailable.Key, result.Error!.Key);
    }

    [Fact]
    public async Task SendAsync_Success_RecordsEmailSentTelemetry()
    {
        using var ctx = BuildSut((_, _) => JsonResponse(HttpStatusCode.Created, """{"messageId":"1"}"""));
        using var listener = new TelemetryCounterListener(NotificationsTelemetry.EmailSent.Name);

        await ctx.Gateway.SendAsync(Message(), CancellationToken.None);

        Assert.Equal(1, listener.Total);
    }

    private static HttpResponseMessage JsonResponse(HttpStatusCode statusCode, string json) =>
        new(statusCode) { Content = new StringContent(json, Encoding.UTF8, "application/json") };

    private sealed class TelemetryCounterListener : IDisposable
    {
        private readonly MeterListener _listener = new();
        private long _total;

        public long Total => Interlocked.Read(ref _total);

        public TelemetryCounterListener(string instrumentName)
        {
            _listener.InstrumentPublished = (instrument, listener) =>
            {
                if (instrument.Meter.Name == NotificationsTelemetry.MeterName && instrument.Name == instrumentName)
                {
                    listener.EnableMeasurementEvents(instrument);
                }
            };
            _listener.SetMeasurementEventCallback<long>((_, measurement, _, _) => Interlocked.Add(ref _total, measurement));
            _listener.Start();
        }

        public void Dispose() => _listener.Dispose();
    }

    private sealed class StubHttpMessageHandler(Func<HttpRequestMessage, string, HttpResponseMessage> respond) : HttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var body = request.Content is null ? string.Empty : await request.Content.ReadAsStringAsync(cancellationToken);
            return respond(request, body);
        }
    }

    private sealed class TestContext : IDisposable
    {
        private readonly HttpClient _httpClient;
        public BrevoEmailGateway Gateway { get; }

#pragma warning disable CA2000 // HttpClient(handler) takes ownership and disposes the handler with disposeHandler:true (the default)
        public TestContext(Func<HttpRequestMessage, string, HttpResponseMessage> respond)
        {
            _httpClient = new HttpClient(new StubHttpMessageHandler(respond))
            {
                BaseAddress = new Uri("https://api.brevo.com/"),
                MaxResponseContentBufferSize = MaxResponseContentBufferBytes,
            };
            var emailOptions = new EmailOptions
            {
                Provider = EmailProvider.Brevo,
                AttemptTimeoutSeconds = 0,
                TotalRequestTimeoutSeconds = 0,
                MaxRetryAttempts = 0,
                MaxPerAddressPerDay = 0,
                MaxPerDay = 0,
                SenderEmail = "no-reply@example.com",
                SenderName = "Sender",
                ThrottleCounterTtlHours = 25,
                Templates = new EmailTemplatesOptions
                {
                    Otp = { ["en"] = new EmailTemplate { Subject = "code {0}", HtmlBody = "<p>{0}</p>" } },
                },
            };
            Gateway = new BrevoEmailGateway(_httpClient, Options.Create(emailOptions), NullLogger<BrevoEmailGateway>.Instance);
        }
#pragma warning restore CA2000

        public void Dispose() => _httpClient.Dispose();
    }
}
