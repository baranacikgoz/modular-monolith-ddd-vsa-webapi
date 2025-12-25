using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class CachingOptions
{
    public bool UseRedis { get; set; }
    public Redis? Redis { get; set; }
}

public class Redis
{
    public required string Host { get; set; }

    public int Port { get; set; }

    public required string Password { get; set; }

    public required string AppName { get; set; }
}

public class CachingOptionsValidator : CustomValidator<CachingOptions>
{
    public CachingOptionsValidator()
    {
#pragma warning disable CS8620
        RuleFor(x => x.Redis)
            .SetValidator(new RedisValidator())
            .When(x => x.UseRedis);
#pragma warning restore CS8620
    }
}

public class RedisValidator : CustomValidator<Redis>
{
    public RedisValidator()
    {
        RuleFor(x => x.Host)
            .NotEmpty()
            .WithMessage("Host should not be empty.");

        RuleFor(x => x.Port)
            .GreaterThan(0)
            .WithMessage("Port should be greater than 0.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password should not be empty.");

        RuleFor(x => x.AppName)
            .NotEmpty()
            .WithMessage("AppName should not be empty.");
    }
}
