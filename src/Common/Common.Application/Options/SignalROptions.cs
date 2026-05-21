using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class SignalROptions
{
    public bool UseRedisBackplane { get; set; }
    public required string RedisConnectionString { get; set; }
}

public class SignalROptionsValidator : CustomValidator<SignalROptions>
{
    public SignalROptionsValidator()
    {
        RuleFor(x => x.RedisConnectionString)
            .NotEmpty()
            .WithMessage("RedisConnectionString is required when UseRedisBackplane is enabled.")
            .When(x => x.UseRedisBackplane);
    }
}
