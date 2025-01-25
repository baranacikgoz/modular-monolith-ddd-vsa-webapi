namespace IAM.Endpoints.Users.VersionNeutral.SelfRegister;

public sealed record Request(
        string Otp,
        string PhoneNumber,
        string Name,
        string LastName,
        string NationalIdentityNumber,
        string BirthDate);
