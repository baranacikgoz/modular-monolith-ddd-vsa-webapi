using System.Linq.Expressions;
using Common.Application.BackgroundJobs;
using Hangfire;

namespace BackgroundJobs;

public class BackgroundJobsService(IBackgroundJobClientV2 client) : IBackgroundJobs
{
    public string Enqueue(Expression<Action> methodCall)
    {
        return client.Enqueue(methodCall);
    }

    public string Enqueue(Expression<Func<Task>> methodCall)
    {
        return client.Enqueue(methodCall);
    }

    public string Enqueue<T>(Expression<Action<T>> methodCall)
    {
        return client.Enqueue(methodCall);
    }

    public string Enqueue<T>(Expression<Func<T, Task>> methodCall)
    {
        return client.Enqueue(methodCall);
    }

    public string Schedule(Expression<Action> methodCall, TimeSpan delay)
    {
        return client.Schedule(methodCall, delay);
    }

    public string Schedule(Expression<Func<Task>> methodCall, TimeSpan delay)
    {
        return client.Schedule(methodCall, delay);
    }

    public string Schedule(Expression<Action> methodCall, DateTimeOffset enqueueAt)
    {
        return client.Schedule(methodCall, enqueueAt);
    }

    public string Schedule(Expression<Func<Task>> methodCall, DateTimeOffset enqueueAt)
    {
        return client.Schedule(methodCall, enqueueAt);
    }

    public string Schedule<T>(Expression<Action<T>> methodCall, TimeSpan delay)
    {
        return client.Schedule(methodCall, delay);
    }

    public string Schedule<T>(Expression<Func<T, Task>> methodCall, TimeSpan delay)
    {
        return client.Schedule(methodCall, delay);
    }

    public string Schedule<T>(Expression<Action<T>> methodCall, DateTimeOffset enqueueAt)
    {
        return client.Schedule(methodCall, enqueueAt);
    }

    public string Schedule<T>(Expression<Func<T, Task>> methodCall, DateTimeOffset enqueueAt)
    {
        return client.Schedule(methodCall, enqueueAt);
    }

    public bool Delete(string jobId)
    {
        return client.Delete(jobId);
    }

    public bool Delete(string jobId, string fromState)
    {
        return client.Delete(jobId, fromState);
    }

    public bool Requeue(string jobId)
    {
        return client.Requeue(jobId);
    }

    public bool Reschedule(string jobId, TimeSpan delay)
    {
        return client.Reschedule(jobId, delay);
    }

    public bool Reschedule(string jobId, DateTimeOffset enqueueAt)
    {
        return client.Reschedule(jobId, enqueueAt);
    }

    public bool Reschedule(string jobId, TimeSpan delay, string fromState)
    {
        return client.Reschedule(jobId, delay, fromState);
    }

    public bool Reschedule(string jobId, DateTimeOffset enqueueAt, string fromState)
    {
        return client.Reschedule(jobId, enqueueAt, fromState);
    }

    public string ContinueJobWith(string parentId, Expression<Action> methodCall)
    {
        return client.ContinueJobWith(parentId, methodCall);
    }

    public string ContinueJobWith<T>(string parentId, Expression<Action<T>> methodCall)
    {
        return client.ContinueJobWith(parentId, methodCall);
    }
}
