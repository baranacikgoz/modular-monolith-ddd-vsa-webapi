using System.Linq.Expressions;
using Common.Application.BackgroundJobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using Products.Infrastructure.Jobs;
using Products.Tests.Support;
using Xunit;

namespace Products.Tests.Jobs;

public class ProductJobsHousekeepingRegistrarTests
{
    private static ProductJobsHousekeepingRegistrar CreateSut(IRecurringBackgroundJobs? recurringJobs, string cron)
    {
        var services = new ServiceCollection();
        if (recurringJobs is not null)
        {
            services.AddSingleton(recurringJobs);
        }

        return new ProductJobsHousekeepingRegistrar(
            services.BuildServiceProvider(),
            TestOptions.Products(o => o.JobHousekeepingCron = cron),
            NullLogger<ProductJobsHousekeepingRegistrar>.Instance);
    }

    [Fact]
    public async Task StartAsync_RegistersTheHousekeepingOnTheConfiguredCron()
    {
        var recurringJobs = Substitute.For<IRecurringBackgroundJobs>();
        var sut = CreateSut(recurringJobs, "30 4 * * * *");

        await sut.StartAsync(CancellationToken.None);

        recurringJobs.Received(1).AddOrUpdate(
            "products-job-housekeeping",
            Arg.Any<Expression<Func<ProductJobsHousekeeping, Task>>>(),
            Arg.Is<Func<string>>(cron => cron() == "30 4 * * * *"));
    }

    [Fact]
    public async Task StartAsync_WithoutTheBackgroundJobsModule_RegistersNothingAndDoesNotThrow()
    {
        var sut = CreateSut(recurringJobs: null, "0 20 * * * *");

        await sut.StartAsync(CancellationToken.None);
    }
}
