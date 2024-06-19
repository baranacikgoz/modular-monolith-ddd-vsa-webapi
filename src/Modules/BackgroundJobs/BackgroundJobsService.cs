using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Common.Application.BackgroundJobs;
using Hangfire;

namespace BackgroundJobs;
public class BackgroundJobsService(IBackgroundJobClientV2 client) : IBackgroundJobs
{
    public string Enqueue(Expression<Action> methodCall) => client.Enqueue(methodCall);

    public string Enqueue(Expression<Func<Task>> methodCall) => client.Enqueue(methodCall);

    public string Enqueue<T>(Expression<Action<T>> methodCall) => client.Enqueue(methodCall);

    public string Enqueue<T>(Expression<Func<T, Task>> methodCall) => client.Enqueue(methodCall);

    public string Schedule(Expression<Action> methodCall, TimeSpan delay) => client.Schedule(methodCall, delay);

    public string Schedule(Expression<Func<Task>> methodCall, TimeSpan delay) => client.Schedule(methodCall, delay);

    public string Schedule(Expression<Action> methodCall, DateTimeOffset enqueueAt) => client.Schedule(methodCall, enqueueAt);

    public string Schedule(Expression<Func<Task>> methodCall, DateTimeOffset enqueueAt) => client.Schedule(methodCall, enqueueAt);

    public string Schedule<T>(Expression<Action<T>> methodCall, TimeSpan delay) => client.Schedule(methodCall, delay);

    public string Schedule<T>(Expression<Func<T, Task>> methodCall, TimeSpan delay) => client.Schedule(methodCall, delay);

    public string Schedule<T>(Expression<Action<T>> methodCall, DateTimeOffset enqueueAt) => client.Schedule(methodCall, enqueueAt);

    public string Schedule<T>(Expression<Func<T, Task>> methodCall, DateTimeOffset enqueueAt) => client.Schedule(methodCall, enqueueAt);

    public bool Delete(string jobId) => client.Delete(jobId);

    public bool Delete(string jobId, string fromState) => client.Delete(jobId, fromState);

    public bool Requeue(string jobId) => client.Requeue(jobId);

    public bool Reschedule(string jobId, TimeSpan delay) => client.Reschedule(jobId, delay);

    public bool Reschedule(string jobId, DateTimeOffset enqueueAt) => client.Reschedule(jobId, enqueueAt);

    public bool Reschedule(string jobId, TimeSpan delay, string fromState) => client.Reschedule(jobId, delay, fromState);

    public bool Reschedule(string jobId, DateTimeOffset enqueueAt, string fromState) => client.Reschedule(jobId, enqueueAt, fromState);

    public string ContinueJobWith(string parentId, Expression<Action> methodCall) => client.ContinueJobWith(parentId, methodCall);

    public string ContinueJobWith<T>(string parentId, Expression<Action<T>> methodCall) => client.ContinueJobWith(parentId, methodCall);
}
