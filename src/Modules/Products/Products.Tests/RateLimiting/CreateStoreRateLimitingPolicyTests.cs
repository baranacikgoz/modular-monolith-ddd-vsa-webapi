using System.Net;
using System.Security.Claims;
using Common.Application.Options;
using Common.Application.Localization.Resources;
using Common.Infrastructure.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Products.Infrastructure.RateLimiting;
using Xunit;

namespace Products.Tests.RateLimiting;

// Regression coverage for the pipeline-order bug (ICURRENTUSER_RATELIMITER_BUG.md in the backend repo):
// UseRateLimiter() now runs after UseAuthentication(), so GetPartition can safely resolve ICurrentUser
// instead of reading a raw claim or throwing on an anonymous caller.
public class CreateStoreRateLimitingPolicyTests
{
    private static readonly Policies.CreateStoreRateLimitingPolicy Sut = new(
        problemDetailsService: null!,
        localizer: null!,
        Options.Create(Build()));

    private static CustomRateLimitingOptions Build() => new()
    {
        Global = new FixedWindow { Limit = 1, PeriodInMs = 1000, QueueLimit = 0 },
        Sms = new FixedWindow { Limit = 1, PeriodInMs = 1000, QueueLimit = 0 },
        Register = new FixedWindow { Limit = 1, PeriodInMs = 1000, QueueLimit = 0 },
        CreateStore = new FixedWindow { Limit = 1, PeriodInMs = 1000, QueueLimit = 0 },
        TokenCreate = new FixedWindow { Limit = 1, PeriodInMs = 1000, QueueLimit = 0 },
        CheckRegistration = new FixedWindow { Limit = 1, PeriodInMs = 1000, QueueLimit = 0 },
        TokenRefresh = new FixedWindow { Limit = 1, PeriodInMs = 1000, QueueLimit = 0 }
    };

    // Mirrors production wiring (Common.Infrastructure/Auth/Setup.cs AddCommonAuth) so ICurrentUser is
    // built from the same ClaimsPrincipal the test hands to HttpContext.User, the way the real DI
    // container builds it once UseAuthentication() has populated it.
    private static DefaultHttpContext ContextWithUser(string? userId)
    {
        var services = new ServiceCollection().AddHttpContextAccessor().AddCommonAuth().BuildServiceProvider();
        var context = new DefaultHttpContext { RequestServices = services };

        if (userId is not null)
        {
            var identity = new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, userId)], "Test");
            context.User = new ClaimsPrincipal(identity);
        }

        services.GetRequiredService<IHttpContextAccessor>().HttpContext = context;
        return context;
    }

    [Fact]
    public void GetPartition_TwoDifferentUsers_ReturnsDifferentPartitionKeys()
    {
        var partitionA = Sut.GetPartition(ContextWithUser("11111111-1111-1111-1111-111111111111"));
        var partitionB = Sut.GetPartition(ContextWithUser("22222222-2222-2222-2222-222222222222"));

        Assert.NotEqual(partitionA.PartitionKey, partitionB.PartitionKey);
    }

    [Fact]
    public void GetPartition_SameUserTwice_ReturnsSamePartitionKey()
    {
        var first = Sut.GetPartition(ContextWithUser("33333333-3333-3333-3333-333333333333"));
        var second = Sut.GetPartition(ContextWithUser("33333333-3333-3333-3333-333333333333"));

        Assert.Equal(first.PartitionKey, second.PartitionKey);
    }

    [Fact]
    public void GetPartition_AnonymousCaller_FallsBackToIpInsteadOfThrowing()
    {
        // The bug this fix closes: the old code threw InvalidOperationException here because
        // ICurrentUser.IdAsString is string.Empty (not null) for an anonymous principal, so the
        // "?? throw" never fired and every anonymous request to an authenticated endpoint 500'd
        // instead of 401'ing via UseAuthorization (which now runs after this policy).
        var context = ContextWithUser(null);
        context.Connection.RemoteIpAddress = IPAddress.Parse("1.2.3.4");

        var partition = Sut.GetPartition(context);

        Assert.Equal("1.2.3.4", partition.PartitionKey);
    }

    [Fact]
    public void GetPartition_TwoAnonymousCallersDifferentIps_ReturnsDifferentPartitionKeys()
    {
        var contextA = ContextWithUser(null);
        contextA.Connection.RemoteIpAddress = IPAddress.Parse("1.2.3.4");
        var contextB = ContextWithUser(null);
        contextB.Connection.RemoteIpAddress = IPAddress.Parse("5.6.7.8");

        var partitionA = Sut.GetPartition(contextA);
        var partitionB = Sut.GetPartition(contextB);

        Assert.NotEqual(partitionA.PartitionKey, partitionB.PartitionKey);
    }
}
