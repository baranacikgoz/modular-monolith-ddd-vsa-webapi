using Common.Application.Validation;
using FluentValidation;

namespace Common.Infrastructure.Options;

public class JwtOptions
{
    public required string Secret { get; set; }

    public required string Issuer { get; set; }

    public required string Audience { get; set; }

    public int AccessTokenExpirationInMinutes { get; set; }

    public int RefreshTokenExpirationInDays { get; set; }
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
            .WithMessage("AccessTokenExpirationInMinutes must be greater than 0.");

        RuleFor(o => o.RefreshTokenExpirationInDays)
            .GreaterThan(0)
            .WithMessage("RefreshTokenExpirationInDays must be greater than 0.");
    }
}
