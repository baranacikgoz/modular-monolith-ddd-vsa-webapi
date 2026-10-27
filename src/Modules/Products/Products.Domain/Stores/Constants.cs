namespace Products.Domain.Stores;

public static class Constants
{
    public const int NameMaxLength = 200;
    public const int DescriptionMaxLength = 1000;
    public const int AddressMaxLength = 1000;
    public const int LogoUrlMaxLength = 512;
    public const int SearchTermMaxLength = 256;

    // Bounds of the applicant's account fields. Complexity of the password is the identity provider's policy.
    public const int ApplicantNameMaxLength = 100;
    public const int ApplicantEmailMaxLength = 255;
    public const int ApplicantPhoneNumberLength = 12;
    public const int ApplicantPasswordMaxLength = 256;
}
