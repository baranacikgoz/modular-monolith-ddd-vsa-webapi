using System.Diagnostics.Metrics;
using System.Net;
using System.Text;
using System.Text.Json;
using Common.Application.Options;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Notifications.Application.Sms;
using Notifications.Infrastructure.Sms.NetGsm;
using Notifications.Infrastructure.Telemetry;
using Xunit;

namespace Notifications.Tests.Sms;

public sealed class NetGsmSmsGatewayTests
{
    private const string PhoneNumber = "905380718209";
    private const long MaxResponseContentBufferBytes = 64 * 1024;

    private static TestContext BuildSut(
        Func<HttpRequestMessage, string, HttpResponseMessage> respond,
        Action<SmsOptions>? configureOptions = null) => new(respond, configureOptions);

    [Fact]
    public async Task SendAsync_Success_ReturnsResultSuccess()
    {
        using var ctx = BuildSut((_, _) =>
            JsonResponse(HttpStatusCode.OK, """{"code":"00","jobid":"123","description":"Success"}"""));

        var result = await ctx.Gateway.SendAsync(new SmsMessage(PhoneNumber, "Your code is 123456", SmsCategory.Transactional), CancellationToken.None);

        Assert.False(result.IsFailure);
    }

    [Fact]
    public async Task SendAsync_Success_RequestBodyHasExpectedShape()
    {
        HttpRequestMessage? capturedRequest = null;
        string? capturedBody = null;
        using var ctx = BuildSut((request, body) =>
        {
            capturedRequest = request;
            capturedBody = body;
            return JsonResponse(HttpStatusCode.OK, """{"code":"00","jobid":"1"}""");
        });

        await ctx.Gateway.SendAsync(new SmsMessage(PhoneNumber, "hello", SmsCategory.Transactional), CancellationToken.None);

        Assert.NotNull(capturedRequest);
        using var doc = JsonDocument.Parse(capturedBody!);
        Assert.Equal("TESTHEADER", doc.RootElement.GetProperty("msgheader").GetString());
        Assert.Equal("5380718209", doc.RootElement.GetProperty("messages")[0].GetProperty("no").GetString());
        Assert.Equal("hello", doc.RootElement.GetProperty("messages")[0].GetProperty("msg").GetString());
        Assert.Equal("0", doc.RootElement.GetProperty("iysfilter").GetString());
        Assert.False(doc.RootElement.TryGetProperty("encoding", out _));
    }

    [Fact]
    public async Task SendAsync_TurkishCharacters_SetsEncodingTr()
    {
        string? capturedBody = null;
        using var ctx = BuildSut((_, body) =>
        {
            capturedBody = body;
            return JsonResponse(HttpStatusCode.OK, """{"code":"00"}""");
        });

        await ctx.Gateway.SendAsync(new SmsMessage(PhoneNumber, "Doğrulama kodunuz", SmsCategory.Transactional), CancellationToken.None);

        using var doc = JsonDocument.Parse(capturedBody!);
        Assert.Equal("tr", doc.RootElement.GetProperty("encoding").GetString());
    }

    [Theory]
    [InlineData("20")]
    [InlineData("70")]
    [InlineData("30")]
    [InlineData("40")]
    [InlineData("50")]
    [InlineData("51")]
    public async Task SendAsync_RejectedCodes_ReturnsRejected(string code)
    {
        using var ctx = BuildSut((_, _) =>
            JsonResponse(HttpStatusCode.NotAcceptable, $$"""{"code":"{{code}}","description":"error"}"""));

        var result = await ctx.Gateway.SendAsync(new SmsMessage(PhoneNumber, "text", SmsCategory.Transactional), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(SmsErrors.Rejected.Key, result.Error!.Key);
    }

    [Theory]
    [InlineData("80")]
    [InlineData("85")]
    public async Task SendAsync_ThrottleCodes_ReturnsThrottled(string code)
    {
        using var ctx = BuildSut((_, _) =>
            JsonResponse(HttpStatusCode.NotAcceptable, $$"""{"code":"{{code}}"}"""));

        var result = await ctx.Gateway.SendAsync(new SmsMessage(PhoneNumber, "text", SmsCategory.Transactional), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(SmsErrors.Throttled.Key, result.Error!.Key);
    }

    [Theory]
    [InlineData("100")]
    [InlineData("101")]
    [InlineData("999")]
    public async Task SendAsync_SystemOrUnknownCodes_ReturnsProviderUnavailable(string code)
    {
        using var ctx = BuildSut((_, _) =>
            JsonResponse(HttpStatusCode.NotAcceptable, $$"""{"code":"{{code}}"}"""));

        var result = await ctx.Gateway.SendAsync(new SmsMessage(PhoneNumber, "text", SmsCategory.Transactional), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(SmsErrors.ProviderUnavailable.Key, result.Error!.Key);
    }

    [Fact]
    public async Task SendAsync_MalformedJson_ReturnsProviderUnavailable()
    {
        using var ctx = BuildSut((_, _) => JsonResponse(HttpStatusCode.OK, "not json"));

        var result = await ctx.Gateway.SendAsync(new SmsMessage(PhoneNumber, "text", SmsCategory.Transactional), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(SmsErrors.ProviderUnavailable.Key, result.Error!.Key);
    }

    [Fact]
    public async Task SendAsync_EmptyBody_ReturnsProviderUnavailable()
    {
        using var ctx = BuildSut((_, _) => JsonResponse(HttpStatusCode.OK, string.Empty));

        var result = await ctx.Gateway.SendAsync(new SmsMessage(PhoneNumber, "text", SmsCategory.Transactional), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(SmsErrors.ProviderUnavailable.Key, result.Error!.Key);
    }

    [Fact]
    public async Task SendAsync_MissingCodeField_ReturnsProviderUnavailable()
    {
        using var ctx = BuildSut((_, _) => JsonResponse(HttpStatusCode.OK, """{"description":"no code field"}"""));

        var result = await ctx.Gateway.SendAsync(new SmsMessage(PhoneNumber, "text", SmsCategory.Transactional), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(SmsErrors.ProviderUnavailable.Key, result.Error!.Key);
    }

    [Fact]
    public async Task SendAsync_ThrowsHttpRequestException_ReturnsProviderUnavailable()
    {
        using var ctx = BuildSut((_, _) => throw new HttpRequestException("connection reset"));

        var result = await ctx.Gateway.SendAsync(new SmsMessage(PhoneNumber, "text", SmsCategory.Transactional), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(SmsErrors.ProviderUnavailable.Key, result.Error!.Key);
    }

    [Fact]
    public async Task SendAsync_NonTurkishPhoneNumber_PassesThroughUnchanged()
    {
        string? capturedBody = null;
        using var ctx = BuildSut((_, body) =>
        {
            capturedBody = body;
            return JsonResponse(HttpStatusCode.OK, """{"code":"00"}""");
        });

        await ctx.Gateway.SendAsync(new SmsMessage("15551234567", "text", SmsCategory.Transactional), CancellationToken.None);

        using var doc = JsonDocument.Parse(capturedBody!);
        Assert.Equal("15551234567", doc.RootElement.GetProperty("messages")[0].GetProperty("no").GetString());
    }

    [Theory]
    [InlineData(SmsCategory.Transactional, "0")]
    [InlineData(SmsCategory.CommercialIndividual, "11")]
    [InlineData(SmsCategory.CommercialMerchant, "12")]
    public async Task SendAsync_Category_SetsExpectedIysFilter(SmsCategory category, string expectedIysFilter)
    {
        string? capturedBody = null;
        using var ctx = BuildSut((_, body) =>
        {
            capturedBody = body;
            return JsonResponse(HttpStatusCode.OK, """{"code":"00"}""");
        });

        await ctx.Gateway.SendAsync(new SmsMessage(PhoneNumber, "text", category), CancellationToken.None);

        using var doc = JsonDocument.Parse(capturedBody!);
        Assert.Equal(expectedIysFilter, doc.RootElement.GetProperty("iysfilter").GetString());
    }

    [Fact]
    public async Task SendAsync_AppNameConfigured_IncludesAppNameField()
    {
        string? capturedBody = null;
        using var ctx = BuildSut(
            (_, body) =>
            {
                capturedBody = body;
                return JsonResponse(HttpStatusCode.OK, """{"code":"00"}""");
            },
            options => options.AppName = "MyApp");

        await ctx.Gateway.SendAsync(new SmsMessage(PhoneNumber, "text", SmsCategory.Transactional), CancellationToken.None);

        using var doc = JsonDocument.Parse(capturedBody!);
        Assert.Equal("MyApp", doc.RootElement.GetProperty("appname").GetString());
    }

    [Fact]
    public async Task SendAsync_AppNameNotConfigured_OmitsAppNameField()
    {
        string? capturedBody = null;
        using var ctx = BuildSut((_, body) =>
        {
            capturedBody = body;
            return JsonResponse(HttpStatusCode.OK, """{"code":"00"}""");
        });

        await ctx.Gateway.SendAsync(new SmsMessage(PhoneNumber, "text", SmsCategory.Transactional), CancellationToken.None);

        using var doc = JsonDocument.Parse(capturedBody!);
        Assert.False(doc.RootElement.TryGetProperty("appname", out _));
    }

    [Fact]
    public async Task SendAsync_TimesOutWithoutCallerCancellation_ReturnsProviderUnavailable()
    {
        using var ctx = BuildSut((_, _) => throw new TaskCanceledException("attempt timeout"));

        var result = await ctx.Gateway.SendAsync(new SmsMessage(PhoneNumber, "text", SmsCategory.Transactional), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(SmsErrors.ProviderUnavailable.Key, result.Error!.Key);
    }

    [Fact]
    public async Task SendAsync_CallerCancelsBeforeCall_PropagatesCancellation()
    {
        using var ctx = BuildSut((_, _) => JsonResponse(HttpStatusCode.OK, """{"code":"00"}"""));
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => ctx.Gateway.SendAsync(new SmsMessage(PhoneNumber, "text", SmsCategory.Transactional), cts.Token));
    }

    [Fact]
    public async Task SendAsync_ResponseExceedsMaxBufferSize_ReturnsProviderUnavailableWithoutThrowing()
    {
        var oversizedJson = $$"""{"code":"00","description":"{{new string('x', (int)MaxResponseContentBufferBytes + 1024)}}"}""";
        using var ctx = BuildSut((_, _) => JsonResponse(HttpStatusCode.OK, oversizedJson));

        var result = await ctx.Gateway.SendAsync(new SmsMessage(PhoneNumber, "text", SmsCategory.Transactional), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(SmsErrors.ProviderUnavailable.Key, result.Error!.Key);
    }

    [Fact]
    public async Task SendAsync_Success_RecordsSmsSentTelemetry()
    {
        using var ctx = BuildSut((_, _) => JsonResponse(HttpStatusCode.OK, """{"code":"00"}"""));
        using var listener = new TelemetryCounterListener(NotificationsTelemetry.SmsSent.Name);

        await ctx.Gateway.SendAsync(new SmsMessage(PhoneNumber, "text", SmsCategory.Transactional), CancellationToken.None);

        Assert.Equal(1, listener.Total);
    }

    [Fact]
    public async Task SendAsync_ProviderErrorCode_RecordsProviderErrorTelemetry()
    {
        using var ctx = BuildSut((_, _) => JsonResponse(HttpStatusCode.NotAcceptable, """{"code":"30"}"""));
        using var listener = new TelemetryCounterListener(NotificationsTelemetry.SmsProviderErrors.Name);

        await ctx.Gateway.SendAsync(new SmsMessage(PhoneNumber, "text", SmsCategory.Transactional), CancellationToken.None);

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
        public NetGsmSmsGateway Gateway { get; }

#pragma warning disable CA2000 // HttpClient(handler) takes ownership and disposes the handler with disposeHandler:true (the default)
        public TestContext(Func<HttpRequestMessage, string, HttpResponseMessage> respond, Action<SmsOptions>? configureOptions = null)
        {
            _httpClient = new HttpClient(new StubHttpMessageHandler(respond))
            {
                BaseAddress = new Uri("https://api.netgsm.com.tr/"),
                MaxResponseContentBufferSize = MaxResponseContentBufferBytes,
            };
            var smsOptions = new SmsOptions
            {
                Provider = SmsProvider.NetGsm,
                MsgHeader = "TESTHEADER",
                Templates = new SmsTemplatesOptions { Otp = { ["en"] = "code {0}" } },
            };
            configureOptions?.Invoke(smsOptions);
            Gateway = new NetGsmSmsGateway(_httpClient, Options.Create(smsOptions), NullLogger<NetGsmSmsGateway>.Instance);
        }
#pragma warning restore CA2000

        public void Dispose() => _httpClient.Dispose();
    }
}
