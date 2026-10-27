using System.Net.Http.Json;
using Common.Application.Caching;
using Common.InterModuleRequests.IAM;
using Common.Tests;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Tests.Endpoints.Otp;

#pragma warning disable CA1707 // Remove the underscores from member name

/// <summary>The verify route stores its token under the purposes other modules verify it under: one for an address without a user, one for an address that has one.</summary>
[Collection("IntegrationTestCollection")]
public class EmailVerificationPurposesTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private sealed record VerifyResponseDto(bool IsRegistered, string EmailVerificationToken);

    [Fact]
    public async Task VerifyEmail_UnknownAddress_StoresTheTokenUnderTheSharedNewAddressPurpose()
    {
        var email = IamTestClient.NewEmail();
        await IamTestClient.SeedOtpAsync(Factory, email, "email_verification");

        using var response = await Factory.CreateClient().PostAsJsonAsync(new Uri("/otp/email/verify", UriKind.Relative),
            new IAM.Endpoints.Otp.VersionNeutral.VerifyEmail.Request { Email = email, Otp = InProcessSendOtpClient.DummyOtp });

        var dto = await response.Content.ReadFromJsonAsync<VerifyResponseDto>(JsonSerializerOptions);
        var cache = Scope.ServiceProvider.GetRequiredService<IFusionCache>();
        var stored = await cache.GetOrDefaultAsync<OtpCacheEntry>(CacheKeys.For.Otp(email, EmailVerificationPurposes.NewAddress));
        Assert.NotNull(stored);
        Assert.Equal(dto!.EmailVerificationToken, stored.Otp);
    }

    [Fact]
    public async Task VerifyEmail_KnownAddress_StoresTheTokenUnderTheSharedExistingAddressPurpose()
    {
        await IamTestClient.SeedOtpAsync(Factory, SeedUsers.StaffEmail, "email_verification");

        using var response = await Factory.CreateClient().PostAsJsonAsync(new Uri("/otp/email/verify", UriKind.Relative),
            new IAM.Endpoints.Otp.VersionNeutral.VerifyEmail.Request { Email = SeedUsers.StaffEmail, Otp = InProcessSendOtpClient.DummyOtp });

        var dto = await response.Content.ReadFromJsonAsync<VerifyResponseDto>(JsonSerializerOptions);
        var cache = Scope.ServiceProvider.GetRequiredService<IFusionCache>();
        var stored = await cache.GetOrDefaultAsync<OtpCacheEntry>(CacheKeys.For.Otp(SeedUsers.StaffEmail, EmailVerificationPurposes.ExistingAddress));
        Assert.NotNull(stored);
        Assert.Equal(dto!.EmailVerificationToken, stored.Otp);
    }
}

#pragma warning restore CA1707
