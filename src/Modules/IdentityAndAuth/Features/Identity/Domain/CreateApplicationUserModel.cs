namespace IdentityAndAuth.Features.Identity.Domain;

internal class CreateApplicationUserModel
{
    public required string FirstName { get; init; } = string.Empty;
    public required string LastName { get; init; } = string.Empty;
    public required string PhoneNumber { get; set; } = string.Empty;
    public required string NationalIdentityNumber { get; set; } = string.Empty;
    public required DateOnly BirthDate { get; set; } = DateOnly.MinValue;
}
