using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class BackgroundJobsOptions
{
    public required bool
        IsServer { get; set; } // Whether this instance of the application should run the Hangfire server or not

    public required int PollingFrequencyInSeconds { get; set; }
    public required string DashboardPath { get; set; }

    // Hangfire keeps its own Postgres pool, separate from the API's shared NpgsqlDataSource.
    // Budget MaxPoolSize against Postgres max_connections across instances; keep WorkerCount <= MaxPoolSize
    // so jobs never block waiting on a connection.
    public required int MaxPoolSize { get; set; }
    public required int WorkerCount { get; set; }
}

public class BackgroundJobsOptionsValidator : CustomValidator<BackgroundJobsOptions>
{
    public BackgroundJobsOptionsValidator()
    {
        RuleFor(x => x.PollingFrequencyInSeconds)
            .GreaterThan(0)
            .WithMessage("Polling frequency should be greater than 0.");

        RuleFor(x => x.DashboardPath)
            .NotEmpty()
            .WithMessage("Dashboard path should not be empty.");

        RuleFor(x => x.MaxPoolSize)
            .GreaterThan(0)
            .WithMessage("MaxPoolSize should be greater than 0.");

        RuleFor(x => x.WorkerCount)
            .GreaterThan(0)
            .WithMessage("WorkerCount should be greater than 0.")
            .LessThanOrEqualTo(x => x.MaxPoolSize)
            .WithMessage("WorkerCount should not exceed MaxPoolSize, or jobs will block waiting on a connection.");
    }
}
