using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.BackgroundJobs;
public interface IRecurringBackgroundJobs
{
    void AddOrUpdate(string recurringJobId, Expression<Action> methodCall, Func<string> cronExpression);
    void AddOrUpdate<T>(string recurringJobId, Expression<Action<T>> methodCall, Func<string> cronExpression);
    void AddOrUpdate(string recurringJobId, Expression<Func<Task>> methodCall, Func<string> cronExpression);
    void AddOrUpdate<T>(string recurringJobId, Expression<Func<T, Task>> methodCall, Func<string> cronExpression);
}
