using System.Net;
using Inventory.Domain.WarehouseConnections;
using Xunit;

namespace Inventory.Tests.WarehouseConnections;

/// <summary>Which addresses a warehouse connection may point at: public ones only, however the address is spelled.</summary>
public class WarehouseAddressPolicyTests
{
    [Theory]
    [InlineData("localhost")]
    [InlineData("LOCALHOST")]
    [InlineData("localhost.")]
    [InlineData("warehouse.localhost")]
    [InlineData("127.0.0.1")]
    [InlineData("127.255.255.254")]
    [InlineData("127.1")]
    [InlineData("2130706433")]
    [InlineData("0x7f.0.0.1")]
    [InlineData("0.0.0.0")]
    [InlineData("0.1.2.3")]
    [InlineData("10.0.0.1")]
    [InlineData("10.255.255.255")]
    [InlineData("172.16.0.1")]
    [InlineData("172.31.255.255")]
    [InlineData("192.168.0.1")]
    [InlineData("192.168.255.255")]
    [InlineData("169.254.169.254")]
    [InlineData("169.254.0.1")]
    [InlineData("100.64.0.1")]
    [InlineData("100.127.255.255")]
    [InlineData("192.0.0.8")]
    [InlineData("198.18.0.1")]
    [InlineData("198.19.255.255")]
    [InlineData("224.0.0.1")]
    [InlineData("240.0.0.1")]
    [InlineData("255.255.255.255")]
    [InlineData("[::1]")]
    [InlineData("[::]")]
    [InlineData("[fe80::1]")]
    [InlineData("[febf::1]")]
    [InlineData("[fec0::1]")]
    [InlineData("[fc00::1]")]
    [InlineData("[fd12:3456:789a::1]")]
    [InlineData("[ff02::1]")]
    [InlineData("[::ffff:127.0.0.1]")]
    [InlineData("[::ffff:10.0.0.1]")]
    [InlineData("[::ffff:169.254.169.254]")]
    [InlineData("[64:ff9b::a00:1]")]
    [InlineData("[2002:a00:1::]")]
    [InlineData("[2001::1]")]
    public void IsPublicHost_NonPublicHost_IsRefused(string host)
    {
        Assert.False(WarehouseAddressPolicy.IsPublicHost(new Uri($"https://{host}/api")));
    }

    [Theory]
    [InlineData("warehouse.example-partner.local")]
    [InlineData("api.warehouse.example.com")]
    [InlineData("notlocalhost.example.com")]
    [InlineData("localhostess.example.com")]
    [InlineData("8.8.8.8")]
    [InlineData("1.1.1.1")]
    [InlineData("172.15.255.255")]
    [InlineData("172.32.0.1")]
    [InlineData("100.63.255.255")]
    [InlineData("100.128.0.1")]
    [InlineData("169.253.1.1")]
    [InlineData("192.169.0.1")]
    [InlineData("198.17.255.255")]
    [InlineData("198.20.0.1")]
    [InlineData("223.255.255.255")]
    [InlineData("[2606:4700:4700::1111]")]
    [InlineData("[::ffff:8.8.8.8]")]
    [InlineData("[64:ff9b::808:808]")]
    [InlineData("[2002:808:808::]")]
    public void IsPublicHost_PublicHost_IsAccepted(string host)
    {
        Assert.True(WarehouseAddressPolicy.IsPublicHost(new Uri($"https://{host}/api")));
    }

    [Theory]
    [InlineData("10.0.0.1", false)]
    [InlineData("::1", false)]
    [InlineData("::ffff:192.168.1.1", false)]
    [InlineData("93.184.216.34", true)]
    [InlineData("2606:2800:220:1:248:1893:25c8:1946", true)]
    public void IsPublicAddress_ResolvedAddress_FollowsTheSameRanges(string address, bool expected)
    {
        Assert.Equal(expected, WarehouseAddressPolicy.IsPublicAddress(IPAddress.Parse(address)));
    }
}
