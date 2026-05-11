using Common.Application.FeatureManagement;
using Xunit;

namespace Common.Tests;

#pragma warning disable CA1515
#pragma warning disable CA1707
public static class FeatureFlagsTests
{
    [Fact]
    public static void Products_NewCheckout_Value()
    {
        Assert.Equal("Products.NewCheckout", FeatureFlags.Products.NewCheckout);
    }

    [Fact]
    public static void Products_V2Pricing_Value()
    {
        Assert.Equal("Products.V2Pricing", FeatureFlags.Products.V2Pricing);
    }

    [Fact]
    public static void Notifications_V2Provider_Value()
    {
        Assert.Equal("Notifications.V2Provider", FeatureFlags.Notifications.V2Provider);
    }

    [Fact]
    public static void Checkout_Variant_Value()
    {
        Assert.Equal("Checkout.Variant", FeatureFlags.Checkout.Variant);
    }
}
#pragma warning restore CA1707
