namespace Common.Infrastructure.Persistence;

public class SeedingCompletionTracker
{
    private readonly TaskCompletionSource _tcs = new(TaskCreationOptions.RunContinuationsAsynchronously);

    public Task WaitForSeedingAsync(CancellationToken cancellationToken = default)
    {
        if (cancellationToken == default)
        {
            return _tcs.Task;
        }

        return _tcs.Task.WaitAsync(cancellationToken);
    }

    internal void MarkComplete()
    {
        _tcs.TrySetResult();
    }

    internal void MarkFaulted(Exception ex)
    {
        _tcs.TrySetException(ex);
    }
}
