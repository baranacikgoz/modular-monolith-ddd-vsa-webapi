namespace IAM.Application.OTP.Features;

public static class OtpCacheKeys
{
    public static string GetKey(string phoneNumber) => $"otp:{phoneNumber}";
}
