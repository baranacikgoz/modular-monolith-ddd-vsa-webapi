using System.Net;
using Common.Application.Options;
using Inventory.Infrastructure.RateLimiting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Inventory.Tests.RateLimiting;

// One bucket per warehouse connection (the {connectionId} route value), never per source IP: a warehouse provider delivers the
// webhooks of all its customers from the same egress addresses, so an IP bucket would let one store's burst throttle the others.
public class WarehouseWebhookRateLimitingPolicyTests
{
    private static readonly Policies.WarehouseWebhookRateLimitingPolicy Sut = new(Options.Create(new CustomRateLimitingOptions
    {
        Global = Window(),
        Sms = Window(),
        Register = Window(),
        CreateStore = Window(),
        TokenCreate = Window(),
        CheckRegistration = Window(),
        TokenRefresh = Window(),
        Email = Window(),
        OtpVerify = Window(),
        StoreApplication = Window(),
        WarehouseWebhook = new FixedWindow { Limit = 20, PeriodInMs = 1000, QueueLimit = 0, FailOpen = true }
    }));

    private static readonly IServiceProvider EmptyServices = new ServiceCollection().BuildServiceProvider();

    private static FixedWindow Window() => new() { Limit = 1, PeriodInMs = 1000, QueueLimit = 0, FailOpen = true };

    private static DefaultHttpContext Context(string? ip, string? connectionId)
    {
        var context = new DefaultHttpContext { RequestServices = EmptyServices };
        if (ip is not null)
        {
            context.Connection.RemoteIpAddress = IPAddress.Parse(ip);
        }

        if (connectionId is not null)
        {
            context.Request.RouteValues["connectionId"] = connectionId;
        }

        return context;
    }

    [Fact]
    public void GetPartition_SameIpDifferentConnections_ReturnsDifferentPartitionKeys()
    {
        var partitionA = Sut.GetPartition(Context("1.2.3.4", "0192a000-0000-7000-8000-000000000001"));
        var partitionB = Sut.GetPartition(Context("1.2.3.4", "0192a000-0000-7000-8000-000000000002"));

        Assert.NotEqual(partitionA.PartitionKey, partitionB.PartitionKey);
    }

    [Fact]
    public void GetPartition_SameConnectionFromDifferentIps_ReturnsSamePartitionKey()
    {
        var first = Sut.GetPartition(Context("9.9.9.9", "0192a000-0000-7000-8000-000000000003"));
        var second = Sut.GetPartition(Context("8.8.8.8", "0192a000-0000-7000-8000-000000000003"));

        Assert.Equal("connectionId:0192a000-0000-7000-8000-000000000003", first.PartitionKey);
        Assert.Equal(first.PartitionKey, second.PartitionKey);
    }

    [Fact]
    public void GetPartition_NoRouteValue_FallsBackToIpPartition()
    {
        var partition = Sut.GetPartition(Context("5.6.7.8", null));

        Assert.Equal("ip:5.6.7.8", partition.PartitionKey);
    }
}
