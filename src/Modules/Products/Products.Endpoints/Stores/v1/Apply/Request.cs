using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.Validation;
using FluentValidation;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.Apply;

public sealed record Request
{
    public required string Email { get; init; }
    public required string PhoneNumber { get; init; }
    public required string Password { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string StoreName { get; init; }
    public required string StoreDescription { get; init; }
    public required string StoreAddress { get; init; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(localizer.Register_Email_NotEmpty)
            .EmailAddress()
            .WithMessage(localizer.Register_Email_Invalid)
            .MaximumLength(Constants.ApplicantEmailMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Stores_v1_Apply_Email_MaxLength,
                Constants.ApplicantEmailMaxLength));

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage(localizer.Register_PhoneNumber_NotEmpty);

        RuleFor(x => x.PhoneNumber)
            .Length(Constants.ApplicantPhoneNumberLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.IAM_PhoneNumber_Length,
                Constants.ApplicantPhoneNumberLength))
            .Must(str => str.All(char.IsDigit))
            .WithMessage(localizer.IAM_PhoneNumber_DigitsOnly)
            .Must(str => str.StartsWith("90", StringComparison.Ordinal))
            .WithMessage(localizer.IAM_PhoneNumber_OnlyTurkey)
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

        // Length and complexity policy lives in the identity provider; a violation comes back as IdentityProviderRejectedUser.
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(localizer.Register_Password_NotEmpty)
            .MaximumLength(Constants.ApplicantPasswordMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Register_Password_MaxLength,
                Constants.ApplicantPasswordMaxLength));

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(localizer.Register_FirstName_NotEmpty)
            .MaximumLength(Constants.ApplicantNameMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Register_FirstName_MaxLength,
                Constants.ApplicantNameMaxLength));

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(localizer.Register_LastName_NotEmpty)
            .MaximumLength(Constants.ApplicantNameMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Register_LastName_MaxLength,
                Constants.ApplicantNameMaxLength));

        RuleFor(x => x.StoreName)
            .NotEmpty()
            .WithMessage(localizer.Stores_v1_Apply_StoreName_NotEmpty)
            .MaximumLength(Constants.NameMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Stores_v1_Apply_StoreName_MaxLength,
                Constants.NameMaxLength));

        RuleFor(x => x.StoreDescription)
            .NotEmpty()
            .WithMessage(localizer.Stores_v1_Apply_StoreDescription_NotEmpty)
            .MaximumLength(Constants.DescriptionMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Stores_v1_Apply_StoreDescription_MaxLength,
                Constants.DescriptionMaxLength));

        RuleFor(x => x.StoreAddress)
            .NotEmpty()
            .WithMessage(localizer.Stores_v1_Apply_StoreAddress_NotEmpty)
            .MaximumLength(Constants.AddressMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, localizer.Stores_v1_Apply_StoreAddress_MaxLength,
                Constants.AddressMaxLength));
    }
}
