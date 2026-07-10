using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class JwtOptions
{
    public required string Secret { get; set; }

    public required string Issuer { get; set; }

    public required string Audience { get; set; }

    public required int AccessTokenExpirationInMinutes { get; set; }

    public required int RefreshTokenExpirationInDays { get; set; }

    /// <summary>
    /// Hard cap on a session's lifetime regardless of how many times its refresh token is rotated.
    /// </summary>
    public required int SessionAbsoluteExpirationInDays { get; set; }

    /// <summary>
    /// Allow-list of recognized client app ids (e.g. "mobile-app", "web-app") accepted on
    /// token Create/Refresh requests as part of the (UserId, DeviceId, ClientId) session key.
    /// </summary>
    public required IReadOnlyCollection<string> AllowedClientIds { get; init; }
}

public class JwtOptionsValidator : CustomValidator<JwtOptions>
{
    public JwtOptionsValidator()
    {
        RuleFor(o => o.Secret)
            .NotEmpty()
            .WithMessage("Secret must not be empty.");

        RuleFor(o => o.Issuer)
            .NotEmpty()
            .WithMessage("Issuer must not be empty.");

        RuleFor(o => o.Audience)
            .NotEmpty()
            .WithMessage("Audience must not be empty.");

        RuleFor(o => o.AccessTokenExpirationInMinutes)
            .GreaterThan(0)
            .WithMessage("AccessTokenExpirationInMinutes must be greater than 0.")
            .LessThanOrEqualTo(15)
            .WithMessage("AccessTokenExpirationInMinutes must not exceed 15 minutes.");

        RuleFor(o => o.RefreshTokenExpirationInDays)
            .GreaterThan(0)
            .WithMessage("RefreshTokenExpirationInDays must be greater than 0.");

        RuleFor(o => o.SessionAbsoluteExpirationInDays)
            .GreaterThan(0)
            .WithMessage("SessionAbsoluteExpirationInDays must be greater than 0.");

        RuleFor(o => o.AllowedClientIds)
            .NotEmpty()
            .WithMessage("AllowedClientIds must not be empty.");
    }
}
