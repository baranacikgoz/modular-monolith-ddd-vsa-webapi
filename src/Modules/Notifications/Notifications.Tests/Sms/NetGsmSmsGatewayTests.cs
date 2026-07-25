using System.Net;
using System.Text;
using System.Text.Json;
using Common.Application.Options;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Notifications.Application.Sms;
using Notifications.Infrastructure.Sms.NetGsm;
using Xunit;

namespace Notifications.Tests.Sms;

public sealed class NetGsmSmsGatewayTests
{
    private const string PhoneNumber = "905380718209";

    private static TestContext BuildSut(Func<HttpRequestMessage, string, HttpResponseMessage> respond) => new(respond);

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

    private static HttpResponseMessage JsonResponse(HttpStatusCode statusCode, string json) =>
        new(statusCode) { Content = new StringContent(json, Encoding.UTF8, "application/json") };

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
        public TestContext(Func<HttpRequestMessage, string, HttpResponseMessage> respond)
        {
            _httpClient = new HttpClient(new StubHttpMessageHandler(respond)) { BaseAddress = new Uri("https://api.netgsm.com.tr/") };
            var options = Options.Create(new SmsOptions { Provider = SmsProvider.NetGsm, MsgHeader = "TESTHEADER" });
            Gateway = new NetGsmSmsGateway(_httpClient, options, NullLogger<NetGsmSmsGateway>.Instance);
        }
#pragma warning restore CA2000

        public void Dispose() => _httpClient.Dispose();
    }
}
