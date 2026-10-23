using Common.Application.Options;
using Host.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Host.Tests;

// The request log redacts a body only when its path and method are listed in requestLogging.json, so a route
// that takes or returns a secret and is missing from the lists writes it to the log, and from there to the log
// store, in clear text. Add a route here when a new endpoint takes or returns a password, a one-time code or a
// token.
public class RequestLoggingSensitivePathsTests
{
    public static TheoryData<string, string> RequestRoutesCarryingASecret => new()
    {
        { "POST", "/tokens" },
        { "POST", "/tokens/email" },
        { "POST", "/tokens/refresh" },
        { "POST", "/otp/login" },
        { "POST", "/otp/registration" },
        { "POST", "/otp/email" },
        { "POST", "/otp/email/verify" },
        { "POST", "/users/register/self" },
        { "POST", "/users/register/self/email" },
    };

    public static TheoryData<string, string> ResponseRoutesCarryingASecret => new()
    {
        { "POST", "/tokens" },
        { "POST", "/tokens/email" },
        { "POST", "/tokens/refresh" },
        { "POST", "/otp/email/verify" },
        { "POST", "/users/register/self" },
        { "POST", "/users/register/self/email" },
    };

    private static RequestLoggingOptions CheckedInOptions()
    {
        var file = Path.Combine(AppContext.BaseDirectory, "Configurations", "requestLogging.json");

        return new ConfigurationBuilder()
                   .AddJsonFile(file, optional: false)
                   .Build()
                   .GetSection(nameof(RequestLoggingOptions))
                   .Get<RequestLoggingOptions>()
               ?? throw new InvalidOperationException($"'{file}' holds no {nameof(RequestLoggingOptions)} section.");
    }

    [Theory]
    [MemberData(nameof(RequestRoutesCarryingASecret))]
    public void CheckedInConfig_RequestRouteCarryingASecret_IsSensitive(string method, string path)
    {
        var rules = CheckedInOptions().SensitiveRequestBodyPaths;

        Assert.True(
            RequestResponseBodyLoggingMiddleware.IsSensitive(new PathString(path), method, rules),
            $"{method} {path} carries a secret in its request body but is not in SensitiveRequestBodyPaths.");
    }

    [Theory]
    [MemberData(nameof(ResponseRoutesCarryingASecret))]
    public void CheckedInConfig_ResponseRouteCarryingASecret_IsSensitive(string method, string path)
    {
        var rules = CheckedInOptions().SensitiveResponseBodyPaths;

        Assert.True(
            RequestResponseBodyLoggingMiddleware.IsSensitive(new PathString(path), method, rules),
            $"{method} {path} carries a secret in its response body but is not in SensitiveResponseBodyPaths.");
    }
}
