namespace Common.Application.Caching;
public static class CacheKeys
{
    public static class For
    {
        public static string Otp(string phoneNumber) => $"otp:{phoneNumber}";
    }
}
