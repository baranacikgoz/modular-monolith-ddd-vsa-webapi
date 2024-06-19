using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Common.Application.BackgroundJobs;
using Hangfire;

namespace BackgroundJobs;
public class RecurringBackgroundJobsService(IRecurringJobManagerV2 recurringJobManager, TimeProvider timeProvider) : IRecurringBackgroundJobs
{
    private readonly RecurringJobOptions _recurringJobOptions = new RecurringJobOptions()
    {
        MisfireHandling = MisfireHandlingMode.Relaxed,
        TimeZone = timeProvider.LocalTimeZone
    };

    public void AddOrUpdate(string recurringJobId, Expression<Action> methodCall, Func<string> cronExpression)
        => recurringJobManager.AddOrUpdate(recurringJobId, methodCall, cronExpression, _recurringJobOptions);

    public void AddOrUpdate<T>(string recurringJobId, Expression<Action<T>> methodCall, Func<string> cronExpression)
        => recurringJobManager.AddOrUpdate(recurringJobId, methodCall, cronExpression, _recurringJobOptions);

    public void AddOrUpdate(string recurringJobId, Expression<Func<Task>> methodCall, Func<string> cronExpression)
        => recurringJobManager.AddOrUpdate(recurringJobId, methodCall, cronExpression, _recurringJobOptions);

    public void AddOrUpdate<T>(string recurringJobId, Expression<Func<T, Task>> methodCall, Func<string> cronExpression)
        => recurringJobManager.AddOrUpdate(recurringJobId, methodCall, cronExpression, _recurringJobOptions);
}
