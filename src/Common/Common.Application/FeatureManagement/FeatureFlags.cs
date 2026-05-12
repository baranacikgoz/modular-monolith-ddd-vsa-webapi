namespace Common.Application.FeatureManagement;

public static class FeatureFlags
{
#pragma warning disable S101
    public static class IAM
    {
        public const string Captcha = "IAM.Captcha";
    }
#pragma warning restore S101

    public static class Products
    {
        public const string NewCheckout = "Products.NewCheckout";
        public const string V2Pricing = "Products.V2Pricing";
    }

    public static class Notifications
    {
        public const string V2Provider = "Notifications.V2Provider";
    }

    public static class Checkout
    {
        public const string Variant = "Checkout.Variant";
    }
}
