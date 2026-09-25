using System.Linq.Expressions;
using Common.Application.BackgroundJobs;
using Common.Application.Options;
using Common.Infrastructure.Persistence.AuditLog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Common.Tests;

#pragma warning disable CA1707 // Remove the underscores from member name
public class AuditLogRetentionJobRegistrarTests
{
    private static AuditLogRetentionJobRegistrar CreateSut(IRecurringBackgroundJobs? recurringJobs, string cron)
    {
        var services = new ServiceCollection();
        if (recurringJobs is not null)
        {
            services.AddSingleton(recurringJobs);
        }

        var options = Options.Create(new AuditLogOptions { RetentionDays = 90, PurgeBatchSize = 5000, RetentionCron = cron });
        return new AuditLogRetentionJobRegistrar(services.BuildServiceProvider(), options, NullLogger<AuditLogRetentionJobRegistrar>.Instance);
    }

    [Fact]
    public async Task StartAsync_RegistersThePurgeOnTheConfiguredCron()
    {
        var recurringJobs = Substitute.For<IRecurringBackgroundJobs>();
        var sut = CreateSut(recurringJobs, "30 4 * * *");

        await sut.StartAsync(CancellationToken.None);

        recurringJobs.Received(1).AddOrUpdate(
            "audit-log-retention-purge",
            Arg.Any<Expression<Func<AuditLogRetentionService, Task>>>(),
            Arg.Is<Func<string>>(cron => cron() == "30 4 * * *"));
    }

    [Fact]
    public async Task StartAsync_WithoutTheBackgroundJobsModule_RegistersNothingAndDoesNotThrow()
    {
        var sut = CreateSut(recurringJobs: null, "0 2 * * *");

        await sut.StartAsync(CancellationToken.None);
    }
}
